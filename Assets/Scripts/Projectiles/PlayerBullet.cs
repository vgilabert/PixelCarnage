using System;
using System.Collections.Generic;
using Enemy;
using Shared;
using StatSystem;
using UnityEngine;
using UpgradeCards.WeaponMods;
using WeaponMods;
using Random = UnityEngine.Random;

namespace Projectiles
{
    public class PlayerBullet : Projectile
    {
        private Damageable _target;
        public Damageable Target { get; set; }
        
        private Vector3 _currentTargetPosition;
        public Vector3 CurrentTargetPosition { get; set; }
        private Vector2 _direction;
        
        // Stats
        private float criticalChance;
        private float criticalDamage;
        private bool _hasMods;

        private List<WeaponModBase> _mods = new(); // List of mods specific to this bullet
        
        protected override void Initialize()
        {
            FindNewTarget();
        }
        
        public void ApplyMods(List<WeaponModBase> mods)
        {
            foreach (var mod in mods)
            {
                var modInstance = mod.Clone(); // Ensure independent instance
                modInstance.ApplyMod(this);
                _mods.Add(modInstance);
            }
        }
        
        public void FindNewTarget()
        {
            _target = TargetFinder.FindClosestTarget(transform.position, ref _currentTargetPosition,
                TargetsHit);
            if (_target == null)
            {
                Destroy(gameObject);
            }
            _direction = _currentTargetPosition - transform.position;
            if (_direction == Vector2.zero)
            {
                Destroy(gameObject);
            }
        }
        
        public override void SetUserStats(StatsData stats)
        {
            Damage = stats[StatType.Attack].Value;
            criticalChance = stats[StatType.CritChance].Value;
            criticalDamage = stats[StatType.CritDamage].Value;
            Force = 0;
        }

        protected override void Move()
        {
            transform.position += (Vector3) _direction.normalized * (Speed * Time.deltaTime);
            transform.up = _direction;
        }
        
        protected override void CheckCollision()
        {
            var hit = Physics2D.Raycast(transform.position, _direction, 0.2f);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out EnemyBase target))
                {
                    if (!TargetsHit.Add(target))
                    {
                        return;
                    }

                    ProcessHit(target);
                }
            }
        }

        public override void ProcessHit(Damageable target)
        {
            float finalDamage = ProcessCriticalChance();
            target.TakeHit(finalDamage, new HitData(_direction, Force));
            
            foreach (var mod in _mods)
            {
                mod.OnHit(target, this);
            }

            // If no mods stopped the bullet, destroy it
            if (_mods.Count == 0 || !_mods.Exists(mod => mod.IsBulletActive))
            {
                Die();
            }
        }
        
        private float ProcessCriticalChance()
        {
            if (Random.value <= criticalChance/100)
            {
                Debug.Log("Critical Hit!");
                AudioManager.Instance.PlaySound(SoundType.CriticalHit);
                return Damage *= criticalDamage/100;
            }
            return Damage;
        }
    }
}