using System.Collections.Generic;
using Enemy;
using Shared;
using StatSystem;
using UnityEngine;
using UpgradeCards.WeaponMods;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        // Projectile stats
        [SerializeField] protected float force = 0f;
        [SerializeField] protected float lifeTime = 2f;
        [SerializeField] protected Projectile projectilePrefab;
        
        // External stats
        protected float BulletSpeed;
        protected float Damage;
        protected float Force;
        protected float CriticalChance;
        protected float CriticalDamage;

        protected Damageable Target;
        protected Vector2 Direction;
        
        protected readonly HashSet<Damageable> TargetsHit = new ();
        protected List<WeaponModBase> Mods = new();
        
        protected bool HasMods => Mods.Count > 0;

        private float _lifeTimer;
        
        // Player variant of Initialize
        public virtual void Initialize(StatsData stats, float bulletSpeed)
        {
            Damage = (int)stats[StatType.Attack].Value;
            Force = force;
            CriticalChance = stats[StatType.CritChance].Value;
            CriticalDamage = stats[StatType.CritDamage].Value;
            BulletSpeed = bulletSpeed;
        }
        
        // Enemy variant of Initialize
        public virtual void Initialize(EnemyStats stats, float bulletSpeed)
        {
            Damage = stats.Damage;
            Force = force;
            CriticalChance = 0;
            CriticalDamage = 0;
            BulletSpeed = bulletSpeed;
        }
        
        public virtual void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }
        
        public void FindNewTarget()
        {
            Vector3 targetPosition = Vector3.zero;
            Target = TargetFinder.FindClosestTarget(transform.position, ref targetPosition, TargetsHit);
            if (Target == null)
            {
                Destroy(gameObject);
            }
            Direction = targetPosition - transform.position;
            if (Direction == Vector2.zero)
            {
                Direction = Vector2.down;
            }
        }

        protected abstract void Move();

        private void Update()
        {
            Move();
            CheckLifeTime();
            CheckCollision();
        }
        
        private void CheckLifeTime()
        {
            _lifeTimer += Time.deltaTime;
            if (_lifeTimer >= lifeTime)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void CheckCollision()
        {
            
        }

        public virtual void OnHit(Damageable target)
        {
            foreach (var mod in Mods)
            {
                mod.OnHit(target, this);
            }
            if (Mods.Count == 0 || !Mods.Exists(mod => mod.IsBulletActive))
            {
                Die();
            }
        }
        
        public void Die() => Destroy(gameObject);
        
        public void ApplyMods(List<WeaponModBase> mods)
        {
            foreach (var mod in mods)
            {
                var modInstance = mod.Clone(); // Ensure independent instance
                modInstance.ApplyMod(this);
                Mods.Add(modInstance);
            }
        }
    }
}