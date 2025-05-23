using System;
using System.Collections;
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

    public Action OnChangeValue;
    private void Awake()
    {
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
        OnChangeValue?.Invoke();

    }

    IEnumerator ChangeValueAboutTime(float value, float time)
    {
        float times = 100;
        float tick = time / times;
        float tickValue = value / times;
        float totalTime = 0f;
        while (time > totalTime)
        {
            yield return new WaitForSeconds(tick);
            totalTime += tick;
            ChangeValue(tickValue);
        }
            yield return null;
    }

    public void ChangeValueStartCoroutine(float value, float time)
    {
        StartCoroutine(ChangeValueAboutTime(value, time));
    }
    
    
    
    
    
    
    
}
