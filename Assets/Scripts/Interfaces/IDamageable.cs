using UnityEngine;

public interface IDamageable
{
    public float CurrentHealth { get; }
    
    public void TakeHit(int damage, HitData hitData = default);
    public void TakeDamage(int damage);
    
    private void Die() { }
}

public struct HitData
{
    public Vector3 Direction;
    public float Force;
    
    public HitData(Vector3 direction, float force)
    {
        Direction = direction == Vector3.zero ? -Vector3.forward : direction;
        Force = force;
    }
}