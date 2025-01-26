using System;
using Shared;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(HitEffectController))]
    public abstract class EnemyBase : Damageable
    {
        [SerializeField] private EnemyStats stats;
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private GameObject hitEffect;
        
        public EnemyStats Stats => stats;
        
        protected Vector3 PlayerPosition;
        protected bool CanAttack;
        
        private Player.Player _playerReference;
        
        private EnemyMovementBase _movementComponent;
        private HitEffectController _hitEffectController;
        
        private float _attackTimer;


        private void Awake()
        {
            _movementComponent = GetComponent<EnemyMovementBase>();
            _hitEffectController = GetComponent<HitEffectController>();
        }

        private void Start()
        {
            _playerReference = SceneManager.Instance.PlayerReference;
            CurrentHealth = stats.MaxHealth;
            _movementComponent.SetSpeed(stats.MoveSpeed);
            TargetFinder.AddTarget(this);
        }
    
        protected virtual void Update()
        {
            PlayerPosition = SceneManager.Instance.PlayerPosition;
            UpdateAttackTimer();
            CheckBodyDamage();
        }
    
        public override void TakeHit(int damage, HitData hitData = default)
        {
            // Process damage
            // TODO: Implement takeDamageCondition
            TakeDamage(damage);
        
            // Push enemy
            if (hitData.Force > 0)
            {
                Vector3 playerDirection = PlayerPosition - transform.position;
                _movementComponent.Push(playerDirection.normalized, hitData.Force);
            }
        
            // Instantiate hit effect
            if (hitEffect != null)
            {
                GameObject fx = Instantiate(hitEffect, transform.position, Quaternion.identity);
                fx.transform.up = hitData.Direction;
            }
            
            // Flash sprite
            if (_hitEffectController != null)
            {
                _hitEffectController.Flash();
            }
        }

        protected override void TakeDamage(int damage)
        {
            AudioManager.Instance.PlaySound(SoundType.EnemyHit);
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
    
        private void UpdateAttackTimer()
        {
            if (!CanAttack)
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= 1/stats.AttackSpeed)
                {
                    CanAttack = true;
                    _attackTimer = 0;
                }
            } 
            else
            {
                PerformAttack();
            }
        }
        
        private void PerformAttack()
        {
            PerformSpecificAttack();
            CanAttack = false;
        }
        
        protected abstract void PerformSpecificAttack();
    
        protected virtual void CheckBodyDamage()
        {
            // Check if player is in range
            Vector2 distance = transform.position - PlayerPosition;
            if (Vector3.Distance(transform.position, PlayerPosition) <= 0.8f)
            {
                _playerReference.TakeHit(stats.Damage, new HitData(distance.normalized, 0f));
            }
        }
    
        protected override void Die()
        {
            AudioManager.Instance.PlaySpatialSound(SoundType.EnemyDeath, transform.position);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            TargetFinder.RemoveTarget(this);
            Destroy(gameObject);
        }
    }

    [Serializable]
    public class EnemyStats
    {
        [SerializeField] private int maxHealth;
        public int MaxHealth => maxHealth;
    
        [SerializeField] private int damage;
        public int Damage => damage;
    
        [SerializeField] private float attackSpeed;
        public float AttackSpeed => attackSpeed;
    
        [SerializeField] private float moveSpeed;
        public float MoveSpeed => moveSpeed;
    }
}