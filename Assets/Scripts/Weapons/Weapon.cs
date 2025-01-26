using System;
using System.Collections;
using Player;
using Projectiles;
using StatSystem;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private int damageMultiplier = 1;
        [SerializeField] private float baseAttackSpeed = 0.1f;
        [SerializeField] private Projectile projectilePrefab;

        private StatsData _userStats;
        protected StatsData UserStats => _userStats;

        private void OnEnable()
        {
            Activate();
        }

        private void OnDisable()
        {
            StopCoroutine(nameof(AttackLoop));
        }

        public void SetUserStats(StatsData stats)
        {
            _userStats = stats;
        }

        public void Activate()
        {
            StartCoroutine(nameof(AttackLoop));
        }
        
        public void Deactivate()
        {
            StopCoroutine(nameof(AttackLoop));
        }

        public Projectile ProjectilePrefab => projectilePrefab;

        protected virtual void Attack()
        {
            if (projectilePrefab == null)
            {
                Debug.LogWarning("Projectile prefab is not set");
                return;
            }
        }

        private IEnumerator AttackLoop()
        {
            if (UserStats == null)
            {
                Debug.LogWarning("User stats are not set");
                yield return null;
            }
            while (true)
            {
                Attack();
                yield return new WaitForSeconds(1 / (baseAttackSpeed * (UserStats[StatType.AttackSpeed].Value==0?1:UserStats[StatType.AttackSpeed].Value)));
            }
        }
    }
}