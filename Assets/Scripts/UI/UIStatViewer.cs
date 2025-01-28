using System.Collections.Generic;
using StatSystem;
using UnityEngine;

namespace UI
{
    public class UIStatViewer : MonoBehaviour
    {
        [SerializeField] private UIStatLine statLinePrefab;
        [SerializeField] private Transform statLineContainer;
    
        private readonly Dictionary<StatType, UIStatLine> statLineDictionary = new ();

        private void OnEnable()
        {
            CharacterStat.OnStatChanged += UpdateStatLine;
        }

        private void Awake()
        {
            if (statLinePrefab == null)
            {
                Debug.LogWarning("StatLinePrefab is null");
                return;
            }
            if (statLineContainer == null)
            {
                Debug.LogWarning("StatLineContainer is null");
                return;
            }
        
            foreach (Transform child in statLineContainer)
            {
                Destroy(child.gameObject);
            }
            foreach (StatType statType in System.Enum.GetValues(typeof(StatType)))
            {
                UIStatLine statLine = Instantiate(statLinePrefab, statLineContainer);
                statLine.SetType = statType;
                float value = SceneManager.Instance.PlayerReference.Stats[statType].Value;
                statLine.UpdateValue(value);
                statLineDictionary.Add(statType, statLine);
            }
        }
    
        private void UpdateStatLine(StatType statType, float value)
        {
            if (statLineDictionary.TryGetValue(statType, out var statLine))
                statLine.UpdateValue(value);
        }
    }
}
