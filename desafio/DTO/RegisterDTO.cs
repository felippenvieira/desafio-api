using System.ComponentModel.DataAnnotations;

namespace desafio_api.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail é inválido.")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}