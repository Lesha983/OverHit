namespace Chillplay.OverHit.Camera
{
    using Cinemachine;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(CameraNoise), menuName = "CameraChoppers/Noise", order = 0)]
    public class CameraNoise : ScriptableObject
    {
        public float AmplitudeGain;

        public float FrequencyGain;
        
        public NoiseSettings ShakeNoise;
    }
}