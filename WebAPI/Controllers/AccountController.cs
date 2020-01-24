using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccesLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;


        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> rolManager,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = rolManager;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResponseDTO), 200)]
        [ProducesResponseType(500)]
        public async Task<object> Login([FromBody] LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                var res = GenerateJwtTokenAsync(model.Email, appUser);
                var aux = res.Result;
                
                return new LoginResponseDTO(aux.ToString(), model.Email);
            }

            return StatusCode(500, "Usuario o contraseña incorrecta.");
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(RegistroDTO), 200)]
        [ProducesResponseType(500)]
        public async Task<object> Register([FromBody] RegistroDTO model)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                //if (model.Nombres == null || model.Nombres.Length < 3)
                //    model.Nombres = "Nombres";

                //if (model.Apellidos == null || model.Apellidos.Length < 3)
                //    model.Apellidos = "Apellidos";

                //if (model.Documento == null || model.Documento.Length < 3)
                //    model.Documento = "Documento";

                //if (model.Apellidos.Length < 3 || model.Nombres.Length < 3 || model.Documento.Length < 3)
                //    return StatusCode(500, "El nombre, apellido y documento del usuario tienen que tener mas de 3 caracteres.");

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    List<string> l = new List<string>();
                    l.Add("USER");
                    await _userManager.AddToRolesAsync(user, l);

                    await _signInManager.SignInAsync(user, false);
                    var aux = GenerateJwtTokenAsync(model.Email, user);

                    return new RegistroResponseDTO(aux.Result.ToString(), model.Email);
                }
                else
                {
                    string errors = "";
                    result.Errors.ToList().ForEach(x => {
                        errors += (x.Description + "|");
                    });
                    return StatusCode(500, "Error no contralado al crear el usuario: " + errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task<object> GenerateJwtTokenAsync(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (string r in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}