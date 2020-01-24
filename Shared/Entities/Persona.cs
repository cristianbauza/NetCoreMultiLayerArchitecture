using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Entities
{
    public class Persona
    {
        public Persona()
        {
            //Contactos = new List<PersonaContacto>();
        }

        public long Id_Persona { get; set; }

        [MaxLength(128, ErrorMessage = "El largo maximo del primer nombre debe ser de 128 caracteres."),
         MinLength(3, ErrorMessage = "El largo mínimo del primer nombre debe ser de 3 caracteres."),
         Required(ErrorMessage = null)]
        public string PrimerNombre { get; set; }

        [MaxLength(128, ErrorMessage = "El largo maximo del segundo nombre debe ser de 128 caracteres.")]
        public string SegundoNombre { get; set; }

        [MaxLength(128, ErrorMessage = "El largo maximo del primer apellido debe ser de 128 caracteres."),
        MinLength(3, ErrorMessage = "El largo mínimo del primer apellido debe ser de 3 caracteres."),
        Required(ErrorMessage = null)]
        public string PrimerApellido { get; set; }

        [MaxLength(128, ErrorMessage = "El largo maximo del segundo apellido debe ser de 128 caracteres.")]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de nacimiento.")]
        public DateTime FechaNacimiento { get; set; }

        [MaxLength(128, ErrorMessage = "El largo maximo del documento debe ser de 128 caracteres.")]
        public string Documento { get; set; }

        [MaxLength(128, ErrorMessage = "El largo maximo del tipo de documento debe ser de 128 caracteres.")]
        public string TipoDocumento { get; set; }

        //public List<PersonaContacto> Contactos { get; set; }
    }
}
