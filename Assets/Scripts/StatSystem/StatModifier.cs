using System;
using UnityEngine;

namespace StatSystem
{
    public enum StatModType
    {
        Flat,
        Percent,
    }
    
    [Serializable]
    public class StatModifier
    {
        [SerializeField] private float value;
        public float Value => value;
        
        [SerializeField] private StatModType type;
        public StatModType Type => type;
        
        [SerializeField] private int order;
        public int Order => order;
        
        [SerializeField] private object source;
        public object Source => source;
        
        [SerializeField] private StatType statType;
        public StatType StatType => statType;
        
        public StatModifier(float value, StatModType type, int order, object source, StatType statType)
        {
            this.value = value;
            this.type = type;
            this.order = order;
            this.source = source;
            this.statType = statType;
        }
        
        public StatModifier(float value, StatModType type, StatType statType) : this(value, type, (int) type, null, statType) { }
        
        public StatModifier(float value, StatModType type, int order, StatType statType) : this(value, type, order, null, statType) { }
        
        public StatModifier(float value, StatModType type, object source, StatType statType) : this(value, type, (int) type, source, statType) { }
    }
}