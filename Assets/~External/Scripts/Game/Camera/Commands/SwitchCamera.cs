namespace Chillplay.OverHit.Camera
{
    using UnityEngine;
    using Zenject;
    using Zenject.Extensions.Commands;
    using Zenject.Extensions.Lazy;

    public abstract class SwitchCamera : LockableCommand
    {
        [Inject]
        public IProvider<GameCamerasService> GameCamerasController { get; set; }
        
        protected abstract CameraType CameraType { get; }

        protected abstract Transform Target { get; }

        protected virtual bool ShouldBlock { get; } = false;
        
        public override void Execute()
        {
            GameCamerasController.Instance.CurrentCamera?.ResetLook();
            GameCamerasController.Instance.SwitchCameraTo(CameraType);
            GameCamerasController.Instance.CurrentCamera.LookAt(Target);
            
            if (ShouldBlock)
            {
                Lock();
                GameCamerasController.Instance.ExecuteAfterBlend(Unlock);
            }
        }
    }
    
    public class SwitchToHomeCamera : SwitchCamera
    {
        protected override CameraType CameraType => CameraType.Home;

        protected override Transform Target => null;
    }

    public class SwitchToGameCamera : SwitchCamera
    {
        protected override CameraType CameraType => CameraType.Game;

        protected override Transform Target => null;
    }

    public class SwitchToEndCamera : SwitchCamera
    {
        protected override CameraType CameraType => CameraType.End;

        protected override Transform Target => null;
    }
}