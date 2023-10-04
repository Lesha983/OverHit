namespace Chillplay.OverHit.Notifications
{
    using System.Collections.Generic;
    using Configs;
    using Retention;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(NotificationsConfig), menuName = "Chillplay/Notifications/Config", order = 0)]
    public class NotificationsConfig : Config
    {
        public List<RetentionNotification> RetentionNotifications;
    }
}