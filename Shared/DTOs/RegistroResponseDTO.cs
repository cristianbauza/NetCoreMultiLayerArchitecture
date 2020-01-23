using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs
{
    public class RegistroResponseDTO
    {
        public RegistroResponseDTO(string token, string email)
        {
            this.token = token;
            this.email = email;
        }

        public string token { get; set; }
        public string email { get; set; }
    }
}
