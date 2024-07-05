using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DesafioUBC.Web.Vue.UI.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        ////RememberMe
        //[Display(Name = "Lembrar de mim?")]
        //public bool RememberMe { get; set; }

        [NotMapped]
        public string ReturnUrl { get; set; }
    }
}
