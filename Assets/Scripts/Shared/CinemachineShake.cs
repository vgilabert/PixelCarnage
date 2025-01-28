using Extensions;
using Unity.Cinemachine;
using UnityEngine;

namespace Shared
{
    public class CinemachineShake : MonoSingleton<CinemachineShake>
    {
        [SerializeField] private CinemachineCamera cinemachineCamera;
    
        private float shakeTimer;
    
        public void Shake(float amplitude, float frequency, float time)
        {
            CinemachineBasicMultiChannelPerlin perlin =
                cinemachineCamera.GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;
            if (perlin == null) return;
            perlin.AmplitudeGain = amplitude;
            perlin.FrequencyGain = frequency;
            shakeTimer = time;
        }
    
        private void Update()
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin perlin =
                    cinemachineCamera.GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;
                if (perlin == null) return;
                perlin.AmplitudeGain = 0;
                perlin.FrequencyGain = 0;
            }
        }
    }
}
