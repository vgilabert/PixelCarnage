using UnityEngine;
using UnityEngine.VFX;

public class VFXBase : MonoBehaviour
{
    protected VisualEffect VfxGraph;
    private bool _hasPlayed;
        
    private void Awake()
    {
        VfxGraph = GetComponent<VisualEffect>();
        if (VfxGraph == null)
        {
            VfxGraph = GetComponentInChildren<VisualEffect>();
        }
    }
        
    private void Update()
    {
        if (VfxGraph.aliveParticleCount == 0 && _hasPlayed)
        {
            Destroy(gameObject);

            _hasPlayed = false;
            return;
        }

        if (VfxGraph.aliveParticleCount > 0)
        {
            _hasPlayed = true;
        }
    }
}