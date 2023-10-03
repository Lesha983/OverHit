namespace Chillplay.OverHit.Camera
{
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(CameraShake), menuName = "CameraChoppers/Shake", order = 0)]
    public class CameraShake : ScriptableObject
    {
        public CameraShakeType Type;
        public CameraNoise Noise;

        public float TransitionTime = 0.3f;
        public float Duration = 0.5f;
    }
    
    public enum CameraShakeType
    {
        Light = 0,
        Heavy = 1
    }
}