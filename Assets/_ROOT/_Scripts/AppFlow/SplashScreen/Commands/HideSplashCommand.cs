namespace Chillplay.AppFlow.SplashScreen
{
    using Zenject;
    using Zenject.Extensions.Commands;

    public class HideSplashCommand : LockableCommand
    {
        [Inject] 
        public ISplash Splash { get; set; }
        
        public override void Execute()
        {
            Splash.Hide();
        }
    }
}