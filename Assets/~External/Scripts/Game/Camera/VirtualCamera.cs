namespace Chillplay.OverHit.Camera
{
    using System.Collections;
    using Cinemachine;
    using DG.Tweening;
    using UnityEngine;

    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class VirtualCamera : MonoBehaviour
    {
        public CameraType Type => type;
        [SerializeField]
        private CameraType type;

        public CinemachineVirtualCamera Camera => cm;
        [SerializeField]
        private CinemachineVirtualCamera cm;
        
        public bool IsActive => gameObject.activeSelf;
        public bool IsShaking { get; private set; }
        
        private Coroutine noiseCoroutine;
        
        public virtual void SetActive(bool isActive)
        {
            if (IsActive != isActive)
            {
                cm.gameObject.SetActive(isActive);
            }
        }
        
        public void LookAt(Transform target)
        {
            cm.m_Follow = cm.m_LookAt = target;
        }
        
        public virtual void SetShake(CameraShake shake)
        {
            var perlin = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (perlin == null)
            {
                IsShaking = false;
                return;
            }

            if (shake == null || shake.Noise == null)
            {
                IsShaking = false;
                perlin.m_AmplitudeGain = 0;
                perlin.m_FrequencyGain = 0;
                return;
            }

            if (noiseCoroutine != null)
            {
                StopCoroutine(noiseCoroutine);
            }

            noiseCoroutine = StartCoroutine(SetNoise(perlin, shake));
        }

        private IEnumerator SetNoise(CinemachineBasicMultiChannelPerlin cameraPerlin, CameraShake shake)
        {
            var halfTime = shake.TransitionTime * 0.5f;
            cameraPerlin.m_NoiseProfile = shake.Noise.ShakeNoise;
            IsShaking = true;

            DOVirtual.Float(cameraPerlin.m_AmplitudeGain, shake.Noise.AmplitudeGain, halfTime,
                v => cameraPerlin.m_AmplitudeGain = v);
            DOVirtual.Float(cameraPerlin.m_FrequencyGain, shake.Noise.FrequencyGain, halfTime,
                v => cameraPerlin.m_FrequencyGain = v);
            
            yield return new WaitForSeconds(halfTime);
            
            DOVirtual.Float(cameraPerlin.m_AmplitudeGain, 0, halfTime, v => cameraPerlin.m_AmplitudeGain = v);
            DOVirtual.Float(cameraPerlin.m_FrequencyGain, 0, halfTime, v => cameraPerlin.m_FrequencyGain = v);
            
            yield return new WaitForSeconds(halfTime);
            yield return new WaitForSeconds(shake.Duration);
            IsShaking = false;
        }
        
        public void ResetLook()
        {
            cm.m_Follow = cm.m_LookAt = null;
        }
    }
}