using Shared;
using StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    public class Player : Damageable
    {
        [SerializeField] private StatsData statsData;
        [SerializeField] private PlayerController2D playerController2D;
        [SerializeField] private GameObject deathEffect;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private float iFramesDuration = 0.3f;
        
        [SerializeField] private InfoBar healthBar;
        [SerializeField] private InfoBar experienceBar;
        [SerializeField] private TextMeshProUGUI levelText;
        
        [Header("Debug")]
        [SerializeField] private bool invincible = false;
        
        public StatsData Stats => statsData;

        private float _iFramesTimer;

        // Components
        private PlayerInput _playerInput;
        private SpriteRenderer _spriteRenderer;
        private HitEffectController _hitEffectController;
        private Weapon _weapon;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _hitEffectController = GetComponent<HitEffectController>();
            _weapon = GetComponent<Weapon>();
        }

        private void OnEnable()
        {
            CharacterStat.OnStatChanged += OnStatChanged;
            PlayerLeveling.OnExperienceChanged += OnExperienceChanged;
            PlayerLeveling.OnLevelUp += OnLevelUp;
        }

        private void OnDisable()
        {
            CharacterStat.OnStatChanged -= OnStatChanged;
            PlayerLeveling.OnExperienceChanged -= OnExperienceChanged;
            PlayerLeveling.OnLevelUp -= OnLevelUp;
        }

        private void Start()
        {
            CurrentHealth = Stats[StatType.MaxHealth].Value;
            if (healthBar != null)
            {
                healthBar.MinValue = 0;
                healthBar.MaxValue = (int)Stats[StatType.MaxHealth].Value;
                healthBar.Value = CurrentHealth;
            }
            playerController2D.SetMaxSpeed(Stats[StatType.MoveSpeed].Value);
            _weapon.SetUserStats(statsData);
        }

        private void Update()
        {
            if (_iFramesTimer < iFramesDuration)
            {
                _iFramesTimer += Time.deltaTime;
            }
        }
        
        public override void TakeHit(float damage, HitData hitData = default)
        {
            if (invincible) return;
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

        protected override void TakeDamage(float damage)
        {
            AudioManager.Instance.PlaySound(SoundType.PlayerHit);
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
        
        protected override void OnHealthChanged()
        {
            if (healthBar != null)
            {
                healthBar.Value = CurrentHealth;
            }
        }
        
        protected override void Die()
        {
            AudioManager.Instance.PlaySound(SoundType.PlayerDeath);
            _weapon.Deactivate();
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            _playerInput.enabled = false;
            _spriteRenderer.enabled = false;
            enabled = false;
            GameManager.Instance.GameOver();
        }
        
        private void OnStatChanged(StatType statType, float value)
        {
            switch (statType)
            {
                case StatType.MaxHealth:
                    var previousMaxHealth = healthBar.MaxValue;
                    healthBar.MaxValue = (int)value;
                    CurrentHealth = Mathf.Clamp(CurrentHealth + (int)value - previousMaxHealth, 0, (int)value);
                    break;
                case StatType.MoveSpeed:
                    Debug.Log(value);
                    playerController2D?.SetMaxSpeed(value);
                    break;
            }
        }
        
        private void OnExperienceChanged(int experienceAmount, int newExperienceCap)
        {
            if (experienceBar == null) return;
            experienceBar.MaxValue = newExperienceCap;
            experienceBar.Value =experienceAmount;
        }
        
        private void OnLevelUp(int level)
        {
            if (levelText == null) return;
            levelText.text = level.ToString();
            
        }
    }
}