using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs
{
    public class LoginResponseDTO
    {
        public LoginResponseDTO(string token, string email)
        {
            this.token = token;
            this.email = email;
        }

        public string token { get; set; }
        public string email { get; set; }
        //public string role { get; set; }
        //public string Apellidos { get; set; }
        //public string Nombres { get; set; }
        //public string Documento { get; set; }
    }
}
