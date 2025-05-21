using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [SerializeField] private BaseCondition health;

    public BaseCondition Health
    {
        get => health; 
        private set => health = value;
    }
    public Action OnHealthChange;

    private void Awake()
    {
        // Health가 Inspector에서 할당되지 않았다면 찾아서 할당
        if (!health)
        {
            health = GameObject.Find("Health")?.GetComponent<BaseCondition>();
            if (!health)
            {
                Debug.LogError("Health component not found!");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (health)
        {
            float value = -damage;
            health.ChangeValue(value);
            OnHealthChange?.Invoke();
        }
    }
    
    
    
    
}
