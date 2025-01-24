using StatSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private StatsData statsData;
        [SerializeField] private PlayerController2D playerController2D;
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private float iFramesDuration = 0.3f;
        
        public StatsData Stats => statsData;

        private int _currentHealth;
        private float _iFramesTimer;

        // Components
        private PlayerInput _playerInput;
        private SpriteRenderer _spriteRenderer;
        private HitEffectController _hitEffectController;
        
        // IDamageable implementation
        public float CurrentHealth => _currentHealth;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _hitEffectController = GetComponent<HitEffectController>();
        }

        private void OnEnable()
        {
            CharacterStat.OnStatChanged += OnStatChanged;
        }

        private void Start()
        {
            _currentHealth = Stats[StatType.MaxHealth].Value;
            playerController2D.SetMaxSpeed(Stats[StatType.MoveSpeed].Value);
        }

        private void Update()
        {
            if (_iFramesTimer < iFramesDuration)
            {
                _iFramesTimer += Time.deltaTime;
            }
        }
        
        public void TakeHit(int damage, HitData hitData = default)
        {
            if (_iFramesTimer < iFramesDuration) return;
            _iFramesTimer = 0;
            
            TakeDamage(damage);
            
            // Instantiate hit effect
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            if (_hitEffectController != null)
            {
                _hitEffectController.Flash();
            }
        }

        public void TakeDamage(int damage)
        {
            AudioManager.Instance.PlaySound(SoundType.PlayerHit);
            Debug.Log($"Player took {damage} damage");
            Debug.Log(_currentHealth);
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
        
        private void Die()
        {
            AudioManager.Instance.PlaySound(SoundType.PlayerDeath);
            _playerInput.enabled = false;
            _spriteRenderer.enabled = false;
            enabled = false;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        
        private void OnStatChanged(StatType statType, int value)
        {
            switch (statType)
            {
                case StatType.MaxHealth:
                    // TODO: Do something
                    break;
                case StatType.MoveSpeed:
                    Debug.Log(value);
                    playerController2D.SetMaxSpeed(value);
                    break;
            }
        }
    }
}