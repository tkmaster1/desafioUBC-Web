public interface INotificationHandler<TEntity> where TEntity : class
{
    Task Handle(TEntity notification);
}