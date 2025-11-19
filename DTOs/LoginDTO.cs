using System.ComponentModel.DataAnnotations;

namespace SportEventManager.DTOs
{
    public class LoginDTO
    {
        [Display(Name = "Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El usuario es obligatorio.")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; } = string.Empty;
    }
}
