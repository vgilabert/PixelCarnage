using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour
{
    [SerializeField] private Color baseColor = Color.green;

    public Color BaseColor
    {
        get => baseColor;
        set
        {
            baseColor = value;
            UpdateBar();
        }
    }

    [SerializeField] private bool useDynamicColors = true;

    public bool UseDynamicColors
    {
        get => useDynamicColors;
        set
        {
            useDynamicColors = value;
            UpdateBar();
        }
    }

    [SerializeField] private Color warningColor = Color.yellow;

    public Color WarningColor
    {
        get => warningColor;
        set
        {
            warningColor = value;
            UpdateBar();
        }
    }

    [SerializeField] private Color criticalColor = Color.red;

    public Color CriticalColor
    {
        get => criticalColor;
        set
        {
            criticalColor = value;
            UpdateBar();
        }
    }

    [SerializeField] private float warningThreshold = 0.5f;

    public float WarningThreshold
    {
        get => warningThreshold;
        set
        {
            warningThreshold = value;
            UpdateBar();
        }
    }

    [SerializeField] private float criticalThreshold = 0.2f;

    public float CriticalThreshold
    {
        get => criticalThreshold;
        set
        {
            criticalThreshold = value;
            UpdateBar();
        }
    }
    
    [SerializeField] private Slider fillSlider;
    
    public Slider FillSlider { get => fillSlider; set => fillSlider = value; }

    [SerializeField] private bool useEaseSlider = true;

    public bool UseEaseSlider
    {
        get => useEaseSlider;
        set => useEaseSlider = value;
    }
    
    [SerializeField] private Slider easeSlider;
    
    public Slider EaseSlider { get => easeSlider; set => easeSlider = value; }
    
    [SerializeField] private Color easeColor = new (0f, 0.5f, 0f, 1f);
    
    public Color EaseColor
    {
        get => easeColor;
        set
        {
            easeColor = value;
            UpdateBar();
        }
    }
    
    [SerializeField] private float easeTime = 0.2f;
    
    public float EaseTime { get => easeTime; set => easeTime = value; }

    [SerializeField] private bool useDisplayText = true;

    public bool UseDisplayText
    {
        get => useDisplayText;
        set
        {
            useDisplayText = value;
            UpdateBar();
        }
    }
    
    [SerializeField] private TextMeshProUGUI displayText;
    
    public TextMeshProUGUI DisplayText { get => displayText; set => displayText = value; }

    [SerializeField] private int minValue = 0;
    public int MinValue
    {
        get => minValue;
        set
        {
            minValue = value;
            UpdateBar();
        }
    }
    
    [SerializeField] private int maxValue = 100;
    public int MaxValue
    {
        get => maxValue;
        set
        {
            maxValue = value;
            UpdateBar();
        }
    }
    
    [SerializeField] private bool useWholeNumbers = true;
    
    public bool UseWholeNumbers
    {
        get => useWholeNumbers;
        set
        {
            useWholeNumbers = value;
            UpdateBar();
        }
    }
    
    private float _value;

    public float Value 
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, minValue, maxValue);
            if (useWholeNumbers)
            {
                _value = Mathf.Round(_value);
            }
            UpdateBar();
            if (useEaseSlider)
            {
                StopAllCoroutines();
                StartCoroutine(UpdateEaseSlider());
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator UpdateEaseSlider()
    {
        float elapsedTime = 0;
        float startValue = easeSlider.value;
        while (elapsedTime < easeTime)
        {
            easeSlider.value = Mathf.Lerp(startValue, Value, elapsedTime / easeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        easeSlider.value = Value;
    } 

    private void UpdateBar()
    {
        if (fillSlider == null || easeSlider == null)
        {
            return;
        }
        fillSlider.minValue = minValue; 
        fillSlider.maxValue = maxValue;
        fillSlider.value = Value;

        if (useEaseSlider) 
        {
            easeSlider.minValue = minValue;
            easeSlider.maxValue = maxValue;
            easeSlider.fillRect.GetComponent<Image>().color = EaseColor;
        }

        if (UseDynamicColors) 
        {
            if (Value <= maxValue * CriticalThreshold)
            {
                fillSlider.fillRect.GetComponent<Image>().color = CriticalColor;
            }
            else if (Value <= maxValue * WarningThreshold)
            {
                fillSlider.fillRect.GetComponent<Image>().color = WarningColor;
            }
            else
            {
                fillSlider.fillRect.GetComponent<Image>().color = BaseColor;
            }
        }
        else
        {
            fillSlider.fillRect.GetComponent<Image>().color = BaseColor;
        }

        if (UseDisplayText && displayText != null)
        {
            //displayText.text = $"{Value}";
            displayText.text = $"{Value}/{maxValue}";
        }
    }
}