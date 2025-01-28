using TMPro;
using UnityEngine;

namespace UI
{
    public class UIStatLine : MonoBehaviour
    {
        private StatSystem.StatType _statType;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _valueText;

        private void Start()
        {
            _nameText.text = _statType.ToString();
        }
        
        public StatSystem.StatType SetType
        {
            set => _statType = value;
        }

        public void UpdateValue(float value)
        {
            _valueText.text = value.ToString();
        }
    }
}