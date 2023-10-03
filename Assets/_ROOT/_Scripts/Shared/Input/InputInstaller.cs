namespace Chillplay.Input
{
    using Core.Modules;
    using Settings;
    using Zenject.Extensions.Commands;

    [Module]
    public class InputInstaller : ModuleInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CustomInputSettings>().FromResourceSettings("Settings/Input");
        }
    }
}
