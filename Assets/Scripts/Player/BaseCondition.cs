using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BaseCondition : MonoBehaviour
{
    [FormerlySerializedAs("_maxValue")] [SerializeField][Range(0f, 100f)]
    private float maxValue;
    public float MaxValue { get => maxValue; set => maxValue = value; }
    [FormerlySerializedAs("_minValue")] [SerializeField][Range(0f, 100f)]
    private float minValue;
    public float MinValue { get => maxValue; set => maxValue = value; }
    [SerializeField][Range(0f, 100f)]
    private float currentValue;
    public float CurrentValue { get => currentValue; set => currentValue = value; }
    [SerializeField][Range(0f, 100f)]
    private float startValue;
    public float StartValue { get => startValue; set => startValue = value; }
    [SerializeField]
    private Image uiImage;
    public Image UIImage { get => uiImage; set => uiImage = value; }
    

    private void Awake()
    {
        uiImage = GetComponent<Image>();
        uiImage.fillAmount = startValue / maxValue;
        currentValue = startValue;
    }

    public void ChangeValue(float value)
    {
        currentValue += value;
        if (currentValue < minValue)
        {
            currentValue = minValue;
        }
        else if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }
        uiImage.fillAmount = currentValue / maxValue;

    }
    
    
    
    
    
}
