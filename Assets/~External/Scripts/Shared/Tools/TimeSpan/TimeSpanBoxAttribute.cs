namespace Chillplay.Tools.TimeSpan
{
    using System;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Field)]
    public class TimeSpanBoxAttribute : PropertyAttribute
    {
        public TimeSpanBoxAttribute(string format)
        {
            Format = format;
        }

        public string Format { get; }
    }
}