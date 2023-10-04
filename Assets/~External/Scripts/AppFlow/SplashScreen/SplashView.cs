namespace Chillplay.AppFlow.SplashScreen
{
    public class SplashView : BaseSplashView
    {
        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}