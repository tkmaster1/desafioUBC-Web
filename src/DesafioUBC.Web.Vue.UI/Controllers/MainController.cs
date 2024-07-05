using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using DesafioUBC.Web.UI.Application.Responses;

namespace DesafioUBC.Web.Vue.UI.Controllers
{
    public class MainController : Controller
    {
        #region Properties

        protected ICollection<string> Erros = new List<string>();

        #endregion

        #region Methods

        protected bool ResponseHasErrors(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors.Mensagens.Any())
            {
                foreach (var mensagem in resposta.Errors.Mensagens)
                    ModelState.AddModelError(string.Empty, mensagem);

                return true;
            }

            return false;
        }

        protected void AdicionarErroValidacao(string mensagem)
        {
            ModelState.AddModelError(string.Empty, mensagem);
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidatedOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AddErrorProcessing(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool ValidatedOperation()
        {
            return !Erros.Any();
        }

        protected void AddErrorProcessing(string erro)
        {
            Erros.Add(erro);
        }

        #endregion
    }
}