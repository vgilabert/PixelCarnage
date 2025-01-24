using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatsData", menuName = "Stats/StatsData")]
    public class StatsData : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<StatType, int> stats;
        
        private Dictionary<StatType, CharacterStat> _actualStats;
        
        private void OnEnable()
        {
            _actualStats = new ();
            foreach (var stat in stats)
            {
                _actualStats.Add(stat.Key, new CharacterStat(stat.Value, stat.Key));
            }
        }
        
        public CharacterStat this[StatType statType]
        {
            get
            {
                if (_actualStats.TryGetValue(statType, out var item))
                {
                    return item;
                }
                else
                {
                    Debug.LogWarning($"StatType {statType} not found in StatsData");
                    return null;
                }
            }
        }
    }
}