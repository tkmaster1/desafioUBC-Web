using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DesafioUBC.Web.UI.Application.Notifications;

namespace DesafioUBC.Web.Vue.UI.Configurations.Filters
{
    public class CustomActionFilterConfig : ActionFilterAttribute, IExceptionFilter
    {
        #region Properties

        private readonly NotificationHandler _notifications;
        private readonly ILogger<CustomActionFilterConfig> _logger;

        #endregion

        #region Constructor

        public CustomActionFilterConfig(INotificationHandler<Notification> notifications
            , ILogger<CustomActionFilterConfig> logger)
        {
            _notifications = (NotificationHandler)notifications;
            _logger = logger;
        }

        #endregion

        #region Methods

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // verifica se tem notificação de erro na api
            if (_notifications.HasNotifications)
            {
                _logger.LogError("[" + DateTime.Now.ToString() + "] " + "[ERROR] - [Sistema] " + _notifications.Notifications.Select(x => x.Message).FirstOrDefault());

                context.HttpContext.Response.StatusCode = 400;
                context.HttpContext.Response.Headers.Add("warning-Notification", _notifications.Notifications.Select(x => x.Message).FirstOrDefault());
            }

            /// verifica se tem erro na aplicação
            if (context.Exception != null)
            {
                _logger.LogError(context.Exception, "[" + DateTime.Now.ToString() + "] " + "[ERROR] - [Sistema] " + context.Exception.ToString());

                var controller = (Controller)context.Controller;
                context.ExceptionHandled = true;
                context.HttpContext.Response.StatusCode = 500;

                if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    context.HttpContext.Response.Headers.Add("error-notification", "Erro desconhecido");
                else
                    context.Result = controller.RedirectToAction("Errors", "Home", new { mensagem = context.Exception?.StackTrace ?? context.Exception?.Message ?? "" });
            }

            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
        }

        #endregion
    }
}