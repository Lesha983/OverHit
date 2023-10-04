namespace Chillplay.AppFlow
{
    using Core;
    using Core.Modules;
    using Gdpr;
    using Notifications.Flow;
    using Saves.Flow;
    using Scenes;
    using SplashScreen;
    using Unity.Services.Core;
    using Unity.Services.Core.Environments;
    using UnityEngine;
    using Zenject;
    using Zenject.Extensions.Commands;

    [Module]
    public class AppFlowInstaller : ModuleInstaller
    {
        public override void InstallBindings()
        {
            InitializeUnityGameServices();
            
            Container.Bind<ISplash>().To<Splash>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameStartedAwaiter>().AsSingle();

            Container.DeclareSignal<GameSceneLoaded>();
            
            Container.BindSignalToCommand<StartApp>()
                .To<InitializeSplashCommand>()
                .To<StartProcessingSavesCommand>()
                .To<TryShowGDPRCommand>()
                .To<RequestNotificationsPermissionCommand>()
                .To<InitializeModulesCommand>()
                .To<WaitAndApplySaveCommand>()
                .To<TryUnblockSavesCommand>()
                .To<SwitchToGameCommand>()
                .To<SignalWrapper<GameSceneLoaded>>()
                .To<HideSplashCommand>();
        }
        
        private async void InitializeUnityGameServices()
        {
            const string environment = "production";
            
            try
            {
                var options = new InitializationOptions()
                    .SetEnvironmentName(environment);

                await UnityServices.InitializeAsync(options);
            }
            catch (System.Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }
    }
}