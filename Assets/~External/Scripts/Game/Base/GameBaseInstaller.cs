namespace Chillplay.OverHit.Base
{
    using Ads;
    using AppFlow.Scenes;
    using Camera;
    using Chillplay.UI.Flow.Commands;
    using Core.Modules;
    using Flow;
    using Input.Flow;
    using Level;
    using LevelLoading;
    using Notifications;
    using UI;
    using Zenject;
    using Zenject.Extensions.Commands;
    using Zenject.Extensions.Lazy;

    [Module]
    public class GameBaseInstaller : ModuleInstaller
    {
        public override void InstallBindings()
        {
            DeclareSignals();
            Bind();
            BindSignalsToCommands();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<PrepareLevel>();
            Container.DeclareSignal<LevelReadyToStart>();
            
            Container.DeclareSignal<LevelStarted>();
            
            Container.DeclareSignal<LevelCompleting>();
            Container.DeclareSignal<LevelCompleted>();
            
            Container.DeclareSignal<LevelFailing>();
            Container.DeclareSignal<LevelFailed>();
            
            Container.DeclareSignal<LevelRestarting>();
            
            Container.DeclareSignal<SwitchingToHome>();
            Container.DeclareSignal<SwitchingToGame>();
            Container.DeclareSignal<SwitchToSettings>();
            Container.DeclareSignal<SkippingHome>();

            Container.DeclareSignal<ILevelEnding>();
            Container.DeclareSignal<ILevelEnded>();

            Container.DeclareSignal<MoveToNextLevel>();
        }

        private void Bind()
        {
            Container.BindInterfacesAndSelfTo<LevelAreaFactory>().AsSingle();
            Container.BindInterfacesTo<Provider<ILevelInstanceParent>>().AsSingle();
            Container.Bind<ILevelBuilder>().To<PrefabLevelBuilder>().AsSingle();
            Container.BindInterfacesTo<LevelProvider>().AsSingle();
            Container.BindInterfacesTo<AttemptProvider>().AsSingle();
            Container.BindInterfacesTo<LevelStateProvider>().AsSingle();
        }

        private void BindSignalsToCommands()
        {
            Container.BindSignalToCommand<GameSceneLoaded>()
                //.To<SetupRetentionNotificationsCommand>()
                .To<SignalWrapper<SwitchingToHome>>();
                //.To<ShowItemTestCommand>();

            Container.BindSignalToCommand<SwitchingToHome>()
                .To<DisableInputCommand>()
                .To<PrepareCurrentLevelCommand>()
                .To<SwitchToHomeCamera>()
                .To<CreateWindow<HomeWindow>>();

            Container.BindSignalToCommand<SkippingHome>()
                .To<CloseWindow<HomeWindow>>()
                .To<PrepareCurrentLevelCommand>()
                .To<SignalWrapper<SwitchingToGame>>();

            Container.BindSignalToCommand<SwitchingToGame>()
                .To<WaitFrameCommand>()
                .To<SignalWrapper<LevelStarted>>();

            Container.BindSignalToCommand<SwitchToSettings>()
                .To<CreateWindow<SettingsWindow>>();

            Container.BindSignalToCommand<PrepareLevel>()
                .To<LoadLevelCommand>();

            Container.BindSignalToCommand<LevelStarted>()
                .To<CreateWindow<GameWindow>>()
                .To<SwitchToGameCamera>()
                .To<StartLevelCommand>()
                .To<EnableInputCommand>();

            Container.BindSignalToCommand<LevelCompleting>()
                .To<WaitAfterLevelCompletingCommand>()
                .To<MoveToNextLevelCommand>()
                .To<CreateWindow<LevelCompletedWindow>>();

            Container.BindSignalToCommand<LevelCompleted>()
                .To<ShowLevelCompletedAdCommand>()
                .To<SignalWrapper<SwitchingToHome>>();

            Container.BindSignalToCommand<LevelFailing>()
                .To<WaitAfterLevelFailingCommand>()
                .To<CreateWindow<LevelFailedWindow>>();

            Container.BindSignalToCommand<LevelFailed>()
                .To<ShowLevelFailedAdCommand>()
                .To<SignalWrapper<SkippingHome>>();

            Container.BindSignalToCommand<ILevelEnding>()
                .To<DisableInputCommand>()
                .To<SwitchToEndCamera>()
                .To<CloseWindow<GameWindow>>();

            Container.BindSignalToCommand<LevelRestarting>()
                .To<WaitFrameCommand>()
                .To<SignalWrapper<SkippingHome>>();

            Container.BindSignalToCommand<MoveToNextLevel>()
                .To<MoveToNextLevelCommand>()
                .To<SignalWrapper<LevelRestarting>>();
        }
    }
}