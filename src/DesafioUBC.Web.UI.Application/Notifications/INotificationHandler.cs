namespace DesafioUBC.Web.UI.Application.Notifications
{
    public interface INotificationHandler<TEntity> where TEntity : class
    {
        Task Handle(TEntity notification);
    }
}