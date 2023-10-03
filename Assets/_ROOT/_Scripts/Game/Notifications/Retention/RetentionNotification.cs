namespace Chillplay.OverHit.Notifications.Retention
{
    using System;
    using Tools.TimeSpan;
    using UnityEngine;

    [Serializable]
    public class RetentionNotification
    {
        public string Title;
        public string Text;

        [Space]
        [TimeSpanBox("{0:hh} hours and {0:mm} minutes")]
        public float TimeInterval;
        [Space]

        public string SmallIcon;
        public string LargeIcon;
    }
}