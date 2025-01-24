using UnityEngine;
using DG.Tweening;

public class HitEffectController : MonoBehaviour
{
    [SerializeField] private float duration = 0.1f;
    
    private readonly int _hitEffectAmount = Shader.PropertyToID("_HitEffectAmount");

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;
    
    private float _lerpAmount;

    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _materials = new Material[_spriteRenderers.Length];
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _materials[i] = _spriteRenderers[i].material;
        }
    }

    public void Flash()
    {
        _lerpAmount = 0f;
        DOTween.To(GetLerpValue, SetLerpValue, 1f, duration).OnUpdate(OnLerpUpdate).OnComplete(OnLerpComplete);
    }

    private void OnLerpUpdate()
    {
        foreach (Material material in _materials)
        {
            material.SetFloat(_hitEffectAmount, GetLerpValue());
        }
    }
    
    private void OnLerpComplete()
    {
        DOTween.To(GetLerpValue, SetLerpValue, 0f, duration).OnUpdate(OnLerpUpdate);
    }

    private float GetLerpValue()
    {
        return _lerpAmount;
    }
    
    private void SetLerpValue(float newValue)
    {
        _lerpAmount = newValue;
    }
}
