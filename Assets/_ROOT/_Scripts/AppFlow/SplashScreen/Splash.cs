namespace Chillplay.AppFlow.SplashScreen
{
    using UnityEngine;

    public class Splash : ISplash
    {
        private SplashView splashView;

        public void Initialize()
        {
            splashView = Object.FindObjectOfType<SplashView>();
        }

        public void Show()
        {
            splashView.Show();
        }

        public void Hide()
        {
            splashView.Hide();
        }
    }
}