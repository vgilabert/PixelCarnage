using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [SerializeField] EnemyStats stats;
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private GameObject hitEffect;
        
        [SerializeField] private Color hitFlashColor = Color.white;
        
        public float CurrentHealth => _currentHealth;
    
        private int _currentHealth;
        private Vector3 _playerPosition;
        private Player.Player _playerReference;
        
        private EnemyMovementBase _movementComponent;
        private SpriteRenderer _spriteRenderer;
        private HitEffectController _hitEffectController;


        private void Awake()
        {
            _movementComponent = GetComponent<EnemyMovementBase>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _hitEffectController = GetComponent<HitEffectController>();
        }

        private void Start()
        {
            _playerReference = SceneManager.Instance.PlayerReference;
            _currentHealth = stats.MaxHealth;
            _movementComponent.SetSpeed(stats.MoveSpeed);
        }
    
        private void Update()
        {
            _playerPosition = SceneManager.Instance.PlayerPosition;
            PerformAttack();
            CheckBodyDamage();
        }
    
        public void TakeHit(int damage, HitData hitData = default)
        {
            // Process damage
            // TODO: Implement takeDamageCondition
            TakeDamage(damage);
        
            // Push enemy
            if (hitData.Force > 0)
            {
                Vector3 playerDirection = _playerPosition - transform.position;
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

        public void TakeDamage(int damage)
        {
            AudioManager.Instance.PlaySound(SoundType.EnemyHit);
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    
        protected virtual void PerformAttack()
        {
            // Attack player
        
        }
    
        protected virtual void CheckBodyDamage()
        {
            // Check if player is in range
            Vector2 distance = transform.position - _playerPosition;
            if (Vector3.Distance(transform.position, _playerPosition) <= 1f)
            {
                _playerReference.TakeHit(stats.Damage, new HitData(distance.normalized, 5f));
            }
        }
    
        protected void Die()
        {
            AudioManager.Instance.PlaySpatialSound(SoundType.EnemyDeath, transform.position);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
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