namespace Chillplay.OverHit.Analytics
{
    using System;
    using System.Collections.Generic;
    using AppFlow;
    using Base;
    using Base.Flow;
    using Base.Level;
    using Base.LevelLoading;
    using Chillplay.Ads;
    using Time;
    using UnityEngine;
    using Utils;
    using Utils.ApplicationEvents;
    using Zenject;

    public class AzurGameEventsRouter : SpecificTagsEventsRouter
    {
        [Inject]
        public SignalBus SignalBus { get; set; }
        
        [Inject]
        public ILevelProvider LevelProvider { get; set; }
        
        [Inject]
        public IAttemptProvider AttemptProvider { get; set; }
        
        [Inject] 
        public ITimeCounterService TimeCounterService { get; set; }
        
        [Inject] 
        public IApplicationEventsTracker ApplicationEventsTracker { get; set; }
        
        [Inject]
        public ILevelStateProvider LevelStateProvider { get; set; }
        
        [Inject]
        public GameStartedAwaiter GameStartedAwaiter { get; set; }
        
        [Inject] 
        public AdsConfig AdsConfig { get; set; }

        private PlayerPrefsStoredValue<bool> shouldSendFinishEvent;
        
        protected override AnalyticsTagsRouterMode RouterMode => AnalyticsTagsRouterMode.Specific;

        private RewardedAdRequested lastRewardedAdRequested;

        private InterstitialAdRequested lastInterAdRequested;
        private DateTime lastInterAdRequestedTime = DateTime.MinValue;

        protected override List<string> SpecifiedTags { get; } = new()
        {
            "am"
        };
        
        protected override void Subscribe()
        {
            SignalBus.Subscribe<LevelStarted>(TrackCurrentLevelStarting);
            SignalBus.Subscribe<LevelFailing>(TrackCurrentLevelFailing);
            SignalBus.Subscribe<LevelCompleting>(TrackCurrentLevelCompleting);
            
            SignalBus.Subscribe<RewardedAdRequested>(OnRewardedRequested);
            SignalBus.Subscribe<InterstitialAdRequested>(OnInterstitialRequested);

            SignalBus.Subscribe<RewardedAdStarted>(OnRewardedStarted);
            SignalBus.Subscribe<InterstitialAdStarted>(OnInterstitialStarted);

            SignalBus.Subscribe<RewardedAdWatched>(OnRewardedWatched);
            SignalBus.Subscribe<InterstitialAdWatched>(OnInterstitialWatched);
            
            shouldSendFinishEvent = new PlayerPrefsStoredValue<bool>("send_finish_event", false);
            ApplicationEventsTracker.ApplicationPause += OnApplicationPause;
            GameStartedAwaiter.ExecuteAfterGameStarted(TryFireSavedEvents);
        }

        private void OnApplicationPause(bool isPaused)
        {
            Debug.Log($"Check send level finish: IsPaused: {isPaused} State: {LevelStateProvider.CurrentState}");
            if (isPaused && LevelStateProvider.CurrentState is >= LevelState.Started and < LevelState.Completed)
            {
                Debug.Log($"Should send level finish in next session");
                shouldSendFinishEvent.Value = true;
            }

            if (!isPaused && LevelStateProvider.CurrentState is >= LevelState.Started and < LevelState.Completed)
            {
                if (shouldSendFinishEvent.Value)
                {
                    Debug.Log($"Should`nt send level finish in next session");
                    shouldSendFinishEvent.Value = false;
                }
            }
        }
        
        private void TryFireSavedEvents()
        {
            Debug.Log($"Should send level finish: {shouldSendFinishEvent.Value}");
            if (shouldSendFinishEvent.Value)
            {
                TrackLevelFinish(LevelProvider.CurrentLevel, "game_closed", 0);
            }
        }

        private void TrackCurrentLevelStarting()
        {
            var eventName = "level_start";
            var eventParams = new Dictionary<string, object>()
            {
                {"level_number", LevelProvider.CurrentLevelNumber},
                {"level_name", LevelProvider.CurrentLevel.LevelConfig.name},
                { "level_count", AttemptProvider.OverallStartedLevels },
                { "level_diff", "default" },
                { "level_random", IsRandom(LevelProvider.CurrentLevel) },
                { "level_type", "normal" },
                { "game_mode", "normal" },
            };
            TrackGameEvent(eventName, eventParams);
        }
        
        private void TrackCurrentLevelCompleting()
        {
            TrackLevelFinish(LevelProvider.CurrentLevel, "win", 100);
        }
        
        private void TrackCurrentLevelFailing()
        {
            TrackLevelFinish(LevelProvider.CurrentLevel, "fail", 0);
        }

        private void TrackLevelFinish(Level level, string result, int progress)
        {
            var eventName = "level_finish";
            var eventParams = new Dictionary<string, object>()
            {
                {"level_number", LevelProvider.CurrentLevelNumber},
                {"level_name", LevelProvider.CurrentLevel.LevelConfig.name},
                { "level_count", AttemptProvider.OverallStartedLevels },
                { "level_diff", "default" },
                { "level_random", IsRandom(LevelProvider.CurrentLevel) },
                { "level_type", "normal" },
                { "game_mode", "normal" },
                { "result", result },
                {"time", TimeCounterService.LevelTimeCounter.Time},
                { "progress", progress },
                { "continue", 0 }
            };
            
            TrackGameEvent(eventName, eventParams);
            shouldSendFinishEvent.Value = false;
        }
        
        private void OnRewardedWatched(RewardedAdWatched signalInfo)
        {
            OnAdWatched(signalInfo.result, signalInfo.placement, AdFormat.Rewarded);
        }

        private void OnInterstitialWatched(InterstitialAdWatched signalInfo)
        {
            OnAdWatched(signalInfo.result, signalInfo.placement, AdFormat.Interstitial);
        }

        private void OnAdWatched(AdWatchResult result, string placement, AdFormat adType)
        {
            var eventName = "video_ads_watch";
            var eventParams = new Dictionary<string, object>()
            {
                { "ad_type", adType.ToAzureId() },
                { "placement", placement.ToAzureId() },
                { "result", result.ToAzureId() },
                { "connection", CheckInternetConnection() },
                { "level_number", LevelProvider.CurrentLevel.LevelNumber },
                { "level_name", LevelProvider.CurrentLevel.LevelConfig.name },
                { "level_count", AttemptProvider.OverallStartedLevels },
                { "level_diff", "default" },
                { "level_random", IsRandom(LevelProvider.CurrentLevel) },
                { "level_type", "normal" },
                { "game_mode", "normal" },
            };
            TrackGameEvent(eventName, eventParams);
        }
        
        private void OnInterstitialRequested(InterstitialAdRequested interstitialRequest)
        {
            if (lastInterAdRequested != null)
            {
                if (!interstitialRequest.isAdAvailable && !lastInterAdRequested.isAdAvailable)
                {
                    if (TimeSinceLastInter() > AdsConfig.GlobalTimeCapping)
                    {
                        OnAdRequested(interstitialRequest.isAdAvailable, interstitialRequest.placement, AdFormat.Interstitial);
                        lastInterAdRequested = interstitialRequest;
                        lastInterAdRequestedTime = DateTime.UtcNow;
                    }
                }
                else
                {
                    OnAdRequested(interstitialRequest.isAdAvailable, interstitialRequest.placement, AdFormat.Interstitial);
                    lastInterAdRequested = interstitialRequest;
                    lastInterAdRequestedTime = DateTime.UtcNow;
                }
            }
            else
            {
                OnAdRequested(interstitialRequest.isAdAvailable, interstitialRequest.placement, AdFormat.Interstitial);
                lastInterAdRequested = interstitialRequest;
                lastInterAdRequestedTime = DateTime.UtcNow;
            }
        }

        private double TimeSinceLastInter()
        {
            return (DateTime.UtcNow - lastInterAdRequestedTime).TotalSeconds;
        }

        private void OnRewardedRequested(RewardedAdRequested rewardedRequested)
        {
            if (lastRewardedAdRequested != null)
            {
                if (rewardedRequested.placement == lastRewardedAdRequested.placement &&
                    !rewardedRequested.isAdAvailable && !lastRewardedAdRequested.isAdAvailable)
                {
                    lastRewardedAdRequested = rewardedRequested;
                }
                else
                {
                    OnAdRequested(rewardedRequested.isAdAvailable, rewardedRequested.placement, AdFormat.Rewarded);
                    lastRewardedAdRequested = rewardedRequested;
                }
            }
            else
            {
                OnAdRequested(rewardedRequested.isAdAvailable, rewardedRequested.placement, AdFormat.Rewarded);
                lastRewardedAdRequested = rewardedRequested;
            }
        }
        
        private void OnAdRequested(bool available, string placement, AdFormat adType)
        {
            var eventName = "video_ads_available";
            var eventParams = new Dictionary<string, object>()
            {
                { "ad_type", adType.ToAzureId() },
                { "placement", placement.ToAzureId() },
                { "result", available ? "success" : "not_available" },
                { "connection", CheckInternetConnection() },
            };
            TrackGameEvent(eventName, eventParams);
        }
        
        private void OnInterstitialStarted(InterstitialAdStarted interstitialStartedSignal)
        {
            OnAdStarted(interstitialStartedSignal.placement, AdFormat.Interstitial);
        }

        private void OnRewardedStarted(RewardedAdStarted rewardedStartedSignal)
        {
            OnAdStarted(rewardedStartedSignal.placement, AdFormat.Rewarded);
        }
        
        private void OnAdStarted(string placement, AdFormat adType)
        {
            var eventName = "video_ads_started";
            var eventParams = new Dictionary<string, object>()
            {
                { "ad_type", adType.ToAzureId() },
                { "placement", placement.ToAzureId() },
                { "result", "start" },
                { "connection", CheckInternetConnection() },
            };
            TrackGameEvent(eventName, eventParams);
        }

        protected override void Unsubscribe()
        {
            SignalBus.Unsubscribe<LevelStarted>(TrackCurrentLevelStarting);
            SignalBus.Unsubscribe<LevelFailing>(TrackCurrentLevelFailing);
            SignalBus.Unsubscribe<LevelCompleting>(TrackCurrentLevelCompleting);
            
            SignalBus.Unsubscribe<RewardedAdRequested>(OnRewardedRequested);
            SignalBus.Unsubscribe<InterstitialAdRequested>(OnInterstitialRequested);

            SignalBus.Unsubscribe<RewardedAdStarted>(OnRewardedStarted);
            SignalBus.Unsubscribe<InterstitialAdStarted>(OnInterstitialStarted);

            SignalBus.Unsubscribe<RewardedAdWatched>(OnRewardedWatched);
            SignalBus.Unsubscribe<InterstitialAdWatched>(OnInterstitialWatched);
            
            ApplicationEventsTracker.ApplicationPause -= OnApplicationPause;
        }
        
        private string IsRandom(Level level)
        {
            return (level.Order.Type == LevelOrderType.Randomized).ToAzureId();
        }
        
        private string CheckInternetConnection()
        {
            return (Application.internetReachability != NetworkReachability.NotReachable).ToAzureId();
        }
    }
    
    public static class AdExt
    {
        public static string ToAzureId(this AdWatchResult adFormat) => adFormat switch
        {
            AdWatchResult.Watched => "watched",
            AdWatchResult.Clicked => "clicked",
            _ => "canceled",
        };

        public static string ToAzureId(this AdFormat adFormat) => adFormat switch
        {
            AdFormat.Interstitial => "interstitial",
            AdFormat.Banner => "banner",
            AdFormat.Rewarded => "rewarded",
            _ => "unknown",
        };

        public static string ToAzureId(this string id)
        {
            return id.ToLowerInvariant();
        }
        
        public static string ToAzureId(this bool v)
        {
            return v.ToString().ToAzureId();
        }
    }
}