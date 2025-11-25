using System.ComponentModel.DataAnnotations;

namespace BestStoreMVC.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Obrigatorio preencher o campo"), MaxLength(100)]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Obrigatorio preencher o campo"), MaxLength(100)]
        public string LastName { get; set; } = "";

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = "";

        [Phone(ErrorMessage = "O numero de telefone não é valido"), MaxLength(20)]
        public string PhoneNumber { get; set; } = "";

        [Required, MaxLength(200)]
        public string Address { get; set; } = "";

        [Required, MaxLength(100)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Obrigatorio preencher o campo")]
        [Compare("Password", ErrorMessage = "Confirme a senha ou as senhas não iguais")]
        public string ConfirmPassword { get; set; } = "";
    }
}
