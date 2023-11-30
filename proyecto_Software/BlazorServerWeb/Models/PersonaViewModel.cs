using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BlazorServerWeb.Models
{
    public class PersonaViewModel
    {
        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(10, ErrorMessage = "El número de documento no puede superar los 10 caracteres.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El número de documento solo puede contener números.")]
        public string NumDoc { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre no puede superar los 30 caracteres.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; }

        [StringLength(30, ErrorMessage = "El segundo nombre no puede superar los 30 caracteres.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "El segundo nombre solo puede contener letras y espacios.")]
        public string? SegundoNombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(60, ErrorMessage = "Los apellidos no pueden superar los 60 caracteres.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Los apellidos solo pueden contener letras y espacios.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El número de celular es obligatorio.")]
        [StringLength(10, ErrorMessage = "El número de celular debe tener 10 caracteres.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El número de celular solo puede contener números.")]
        public string Celular { get; set; }

        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "El formato de la imagen no es válido.")]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = "La imagen no puede superar los 2 MB.")]
        public byte[]? Foto { get; set; }

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public sealed class MaxFileSizeAttribute : ValidationAttribute
        {
            private readonly int _maxFileSize;

            public MaxFileSizeAttribute(int maxFileSize)
            {
                _maxFileSize = maxFileSize;
            }

            public override bool IsValid(object value)
            {
                if (value is IFormFile file)
                {
                    return file.Length <= _maxFileSize;
                }

                return true; // Si no es un archivo, se considera válido
            }

            public override string FormatErrorMessage(string name)
            {
                return $"El tamaño máximo del archivo debe ser {_maxFileSize / (1024 * 1024)} MB.";
            }
        }
    }
}
