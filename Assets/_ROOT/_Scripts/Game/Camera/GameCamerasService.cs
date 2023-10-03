namespace Chillplay.OverHit.Camera
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Cinemachine;
    using UnityEngine;
    using Zenject.Extensions.Lazy;

    public class GameCamerasService : MonoProvided<GameCamerasService>
    {
        public Camera MainCamera { get; private set; }
        
        public CameraType CurrentCameraType { get; private set; } = CameraType.None;

        public VirtualCamera CurrentCamera { get; private set; }
        
        [SerializeField]
        private List<CameraShake> shakes;
        [SerializeField]
        private List<VirtualCamera> cameras;

        private CinemachineBrain cinemachineBrain;
        private void Awake()
        {
            MainCamera = Camera.main;
            cinemachineBrain = MainCamera.GetComponent<CinemachineBrain>();
        }
        
        public void SwitchCameraTo(CameraType type)
        {
            foreach (var cam in cameras)
            {
                var active = cam.Type == type;
                if (active) CurrentCamera = cam;
                cam.SetActive(active);
            }
            CurrentCameraType = type;
        }
        
        public void ShakeCamera(CameraShakeType shakeType)
        {
            var cameras = 
                this.cameras.Where(c => c.IsActive && !c.IsShaking);
            var shake = shakes.FirstOrDefault(sh => sh.Type == shakeType);
            
            if(shake == null) return;

            foreach (var camera in cameras)
            {
                camera.SetShake(shake);
            }
        }
        
        public VirtualCamera GetCamera(CameraType type)
        {
            return cameras.First(c => c.Type == type);
        }
        
        public void ExecuteAfterBlend(Action action)
        {
            var coroutine = ExecuteAfterBlendCoroutine(action);
            StartCoroutine(coroutine);
        }

        private IEnumerator ExecuteAfterBlendCoroutine(Action action)
        {
            yield return null;
            yield return null;
            while (cinemachineBrain.IsBlending)
            {
                yield return null;
            }
            action?.Invoke();
        }
    }
}
