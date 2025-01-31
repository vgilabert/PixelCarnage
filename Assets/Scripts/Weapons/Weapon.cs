using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Projectiles;
using Shared;
using StatSystem;
using UnityEngine;
using UpgradeCards.WeaponMods;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Weapon Stats")]
        [SerializeField] private int damageMultiplier = 1;
        [SerializeField] private float attackSpeedMultiplier = 1f;
        [SerializeField] private float range = 20f;
        [SerializeField] private float projectileSpeed = 15f;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private bool targetIsPlayer = false;
        
        [Header("Debug")]
        [SerializeField] bool debug = false;

        protected StatsData UserStats;
        protected Damageable Target;
        protected Vector3 TargetPosition;
        protected int ProjectileCount = 1;

        private readonly List<WeaponModBase> _mods = new();
        public List<WeaponModBase> Mods => _mods;

        private TargetMarker _targetMarker;

        private void OnDisable()
        {
            StopCoroutine(nameof(FiringLoop));
        }

        protected virtual void Start()
        {
            _targetMarker = TargetMarker.Instance;
        }
        
        protected virtual void Update()
        {
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            if (targetIsPlayer)
            {
                TargetPosition = SceneManager.Instance.PlayerPosition;
                return;
            }
            Target = TargetFinder.FindClosestTarget(transform.position, ref TargetPosition, null, range);
            if (Target != null)
            {
                if (_targetMarker == null) return;
                
                _targetMarker.gameObject.SetActive(true);
                _targetMarker.transform.position = TargetPosition;
            }
            else
            {
                if (_targetMarker == null) return;
                
                _targetMarker.gameObject.SetActive(false);
            }
        }

        public void SetUserStats(StatsData stats)
        {
            UserStats = stats;
        }
        
        public void AddMod(WeaponModBase weaponMod)
        {
            _mods.Add(weaponMod);
            weaponMod.ApplyMod(this);
        }

        public void Activate()
        {
            if (projectilePrefab == null)
            {
                Debug.LogWarning($"Projectile prefab is not set for {name}. Weapon will not fire.");
                return; 
            }
            StartCoroutine(nameof(FiringLoop));
        }
        
        public void Deactivate()
        {
            StopCoroutine(nameof(FiringLoop));
        }
        
        private IEnumerator FiringLoop()
        {
            while (true)
            {
                if (Target != null)
                {
                    Fire();
                }
                
                // Prevents division by zero
                if (UserStats[StatType.AttackSpeed].Value == 0)
                    yield return new WaitForSeconds(1 / attackSpeedMultiplier);
                
                // Cooldown between shots
                yield return new WaitForSeconds(1 / (attackSpeedMultiplier * UserStats[StatType.AttackSpeed].Value));
            }
        }
        
        private void Fire()
        {
            InstantiateProjectiles(projectilePrefab, TargetPosition - transform.position, UserStats, projectileSpeed);
        }

        protected abstract void InstantiateProjectiles(Projectile prefab, Vector2 direction, StatsData stats, float projectileSpeed);
        
        // add projectile functionality
        public virtual void SetProjectileCount(int count)
        {
            ProjectileCount = count;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
            Debug.DrawLine(transform.position, TargetPosition, Color.red);
        }
    }
}