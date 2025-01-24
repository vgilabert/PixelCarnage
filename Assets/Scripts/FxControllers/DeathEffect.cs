using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class DeathEffect : VFXBase
{
    [SerializeField] private Color color;

    private void Start()
    {
        VfxGraph.SetVector4("Color", color);
    }
}
