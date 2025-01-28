using System.Collections;
using System.Collections.Generic;
using Projectiles;
using Shared;
using StatSystem;
using UnityEngine;
using WeaponMods;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private int damageMultiplier = 1;
        [SerializeField] private float baseAttackSpeed = 0.1f;
        [SerializeField] private Projectile projectilePrefab;

        private StatsData _userStats;
        protected StatsData UserStats => _userStats;
        
        private readonly List<WeaponModBase> _mods = new();
        public List<WeaponModBase> Mods => _mods;
        
        private TargetMarker _targetMarker;

        private void OnEnable()
        {
            Activate();
        }

        private void OnDisable()
        {
            StopCoroutine(nameof(AttackLoop));
        }

        protected virtual void Start()
        {
            _targetMarker = TargetMarker.Instance;
        }
        
        protected virtual void Update()
        {
            Vector3 targetTransform = Vector3.zero;
            TargetFinder.FindClosestTarget(transform.position, ref targetTransform);
            if (_targetMarker != null)
            {
                if (targetTransform != Vector3.zero)
                {
                    _targetMarker.gameObject.SetActive(true);
                    _targetMarker.transform.position = targetTransform;
                }
                else
                {
                    _targetMarker.gameObject.SetActive(false);
                }
            }
        }

        public void SetUserStats(StatsData stats)
        {
            _userStats = stats;
        }
        
        public void AddMod(WeaponModBase weaponMod)
        {
            _mods.Add(weaponMod);
        }

        public void Activate()
        {
            StartCoroutine(nameof(AttackLoop));
        }
        
        public void Deactivate()
        {
            StopCoroutine(nameof(AttackLoop));
        }
        
        private IEnumerator AttackLoop()
        {
            if (UserStats == null)
            {
                yield return null;
            }
            while (true)
            {
                Attack();
                yield return new WaitForSeconds(1 / (baseAttackSpeed * (UserStats[StatType.AttackSpeed].Value==0?1:UserStats[StatType.AttackSpeed].Value)));
            }
        }
        
        protected virtual void Attack()
        {
            if (projectilePrefab == null)
            {
                Debug.LogWarning("Projectile prefab is not set");
                return; 
            }
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            SetUpProjectile(projectile);
        }
        
        protected virtual void SetUpProjectile(Projectile projectile)
        {
            projectile.SetUserStats(UserStats);
        }
    }
}