namespace Chillplay.OverHit.Analytics
{
    using System.Collections.Generic;
    using System.Linq;
    using Chillplay.Analytics.EventRouters;
    using UnityEngine;
    
    public enum AnalyticsTagsRouterMode
    {
        None,
        Specific,
        All
    }

    public abstract class SpecificTagsEventsRouter : AnalyticEventsRouter
    {
        protected abstract AnalyticsTagsRouterMode RouterMode { get; }

        protected abstract List<string> SpecifiedTags { get; }

        protected void TrackGameEvent(string eventName, IDictionary<string, object> parameters)
        {
            if (RouterMode == AnalyticsTagsRouterMode.All)
            {
                Analytics.TrackGameEvent(eventName, parameters);
                return;
            }

            if (RouterMode == AnalyticsTagsRouterMode.Specific && SpecifiedTags != null)
            {
                if (!SpecifiedTags.Any())
                {
                    Debug.LogWarning($"Router mode set to {RouterMode}, but any tags were specified!");
                }
                foreach (var tag in SpecifiedTags)
                {
                    Analytics.TrackGameEventWithTag(tag, eventName, parameters);
                }
                return;
            }
        }

        protected void TrackGameEvent(string eventName)
        {
            if (RouterMode == AnalyticsTagsRouterMode.All)
            {
                Analytics.TrackGameEvent(eventName);
                return;
            }

            if (RouterMode == AnalyticsTagsRouterMode.Specific && SpecifiedTags != null)
            {
                if (!SpecifiedTags.Any())
                {
                    Debug.LogWarning($"Router mode set to {RouterMode}, but any tags were specified!");
                }
                foreach (var tag in SpecifiedTags)
                {
                    Analytics.TrackGameEventWithTag(tag, eventName);
                }
                return;
            }
        }
    }
}