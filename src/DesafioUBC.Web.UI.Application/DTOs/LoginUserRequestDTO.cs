using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioUBC.Web.UI.Application.DTOs
{
    public class LoginUserRequestDTO
    {
        //[Required(ErrorMessage = "O campo {0} é obrigatório!")]
        //[EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        //[Display(Name = "E-mail")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório!")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Senha")]
        public string Password { get; set; }

        //MeLembre
        //[Display(Name = "Lembrar de mim?")]
        public bool RememberMe { get; set; }

        [NotMapped]
        public string ReturnUrl { get; set; }
    }
}
