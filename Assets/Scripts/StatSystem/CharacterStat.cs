using System;
using System.Collections.Generic;

namespace StatSystem
{
    public enum StatType
    {
        Attack,
        AttackSpeed,
        CritChance,
        CritDamage,
        MaxHealth,
        DamageReduction,
        MoveSpeed
    }
    
    public class CharacterStat
    {
        private readonly float _baseValue;
        private StatType _statType;
        
        public static Action<StatType, float> OnStatChanged;

        public float Value
        {
            get
            {
                if (_isDirty || _lastBaseValue != _baseValue)
                {
                    _isDirty = false;
                    _value = CalculateFinalValue();
                    _lastBaseValue = _baseValue;
                    return _value;
                }
                return _value;
            }
        }

        private bool _isDirty = true;
        private float _value;
        private float _lastBaseValue = float.MinValue;

        private readonly List<StatModifier> _statModifiers;

        public CharacterStat(float baseValue, StatType statType)
        {
            _baseValue = baseValue;
            _statType = statType;
            _statModifiers = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier)
        {
            _statModifiers.Add(modifier);
            _statModifiers.Sort(CompareModifierOrder);
            _isDirty = true;
            OnStatChanged?.Invoke(_statType, Value);
        }
        
        private int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0;
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _statModifiers.Remove(modifier);
            _isDirty = true;
            OnStatChanged?.Invoke(_statType, Value);
        }

        private float CalculateFinalValue()
        {
            float finalValue = _baseValue;
            float sumPercentAdd = 0;
            
            for (int i = 0; i < _statModifiers.Count; i++)
            {
                StatModifier modifier = _statModifiers[i];

                if (modifier.Type == StatModType.Flat)
                {
                    finalValue += modifier.Value;
                }
                else if (modifier.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += modifier.Value/100f;
                    
                    if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (modifier.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + modifier.Value/100f;
                }
            }

            return finalValue;
        }
    }
}