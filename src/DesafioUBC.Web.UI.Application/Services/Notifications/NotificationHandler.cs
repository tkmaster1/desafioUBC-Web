namespace DesafioUBC.Web.UI.Application.Services.Notifications
{
    public class NotificationHandler : INotificationHandler<Notification>
    {
        #region properties

        private List<Notification> _notifications;

        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public bool HasNotifications => _notifications.Any();

        #endregion

        #region Constructor

        public NotificationHandler() => _notifications = new List<Notification>();

        #endregion

        #region Methods

        public Task Handle(Notification notification)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _notifications = new List<Notification>();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}