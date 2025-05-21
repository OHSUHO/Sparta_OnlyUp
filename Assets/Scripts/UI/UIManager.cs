using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingleTon<UIManager>   
{
    private Player _player;
    [SerializeField] private Image healthBar;

    private void Awake()
    {
        _player = CharacterManager.Instance.Player;
        //HealthBar 예외처리코드
        if (!healthBar)
        {
            healthBar = GameObject.Find("Health")?.GetComponent<Image>();
            if (!healthBar)
            {
                Debug.LogError("Health Image component not found!");
            }
        }
    }

    private void Start()
    {
        healthBar = _player.PlayerCondition.Health.GetComponent<Image>();
        //Player할당 예외처리코드
        if (_player && _player.PlayerCondition)
        {
            _player.PlayerCondition.OnHealthChange += UpdateHealthBar;
            // 초기 healthBar 값 설정
            UpdateHealthBar();
        }
        else
        {
            Debug.LogError("Player or PlayerCondition is not properly initialized!");
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar && _player)
        {
            healthBar.fillAmount = _player.PlayerCondition.Health.CurrentValue / _player.PlayerCondition.Health.MaxValue;
        }
        
        
        
    }
}
