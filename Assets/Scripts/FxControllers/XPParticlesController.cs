using System.Collections.Generic;
using Extensions;
using Player;
using UnityEngine;

public class XpParticlesController : MonoSingleton<XpParticlesController>
{
    // References
    public ParticleSystem ps;
    public PlayerLeveling playerLeveling;
    
    private readonly List<ParticleSystem.Particle> _particles = new();
    
    public void SpawnParticles(int amount, Vector3 position)
    {
        transform.position = position;
        ps.Emit(amount);
    }

    private void OnParticleTrigger()
    {
        int triggeredParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, _particles);
        
        for (int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle p = _particles[i];
            p.remainingLifetime = 0;
            _particles[i] = p;
            AudioManager.Instance.PlaySound(SoundType.ParticlePickup);
            playerLeveling.IncreaseExperience(1);
        }
        
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _particles);
    }
}
