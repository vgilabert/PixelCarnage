using UnityEngine;

namespace Shared
{
    public abstract class Damageable : MonoBehaviour
    {
        private int _currentHealth;

        public virtual float CurrentHealth
        {
            get => _currentHealth;
            protected set
            {
                _currentHealth = (int)value;
                OnHealthChanged();
            }
        }

        public abstract void TakeHit(int damage, HitData hitData = default);
        protected abstract void TakeDamage(int damage);
        
        protected virtual void OnHealthChanged() { }
    
        protected abstract void Die();
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
}