using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioUBC.Web.UI.Application.DTOs
{
    public class LoginUserRequestDTO
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        [NotMapped]
        public string ReturnUrl { get; set; }
    }
}
