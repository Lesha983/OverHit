namespace Chillplay.OverHit.Notifications
{

    using System;
    using Chillplay.Notifications;
    using Retention;
    using Zenject;
    using Zenject.Extensions.Commands;

    public class SetupRetentionNotificationsCommand : LockableCommand
    {
        [Inject]
        private NotificationsConfig Config { get; set; }

        [Inject]
        private INotificationsProvider NotificationsProvider { get; set; }

        public override void Execute()
        {
            for (int i = 0; i < Config.RetentionNotifications.Count; i++)
            {
                var notificationData = Config.RetentionNotifications[i];
                NotificationsProvider.CancelBy(i);
                var notification = CreateNotification(notificationData, i);
                NotificationsProvider.SetUpNotification(notification);
            }
        }

        private Notification CreateNotification(RetentionNotification data, int id)
        {
            var showTime = DateTime.UtcNow.Add(TimeSpan.FromHours(data.TimeInterval));

            return new Notification()
            {
                Id = id,
                Title = data.Title,
                Text = data.Text,
                ShowTime = showTime,
                SmallIcon = data.SmallIcon,
                LargeIcon = data.LargeIcon
            };
        }
    }
}