using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : SingleTon<UIManager>   
{
    public Inventory uiInventoryScript;
    [SerializeField]private Player _player;
    [SerializeField]private Image healthBar;
    [SerializeField]private TextMeshProUGUI prompt;
    [SerializeField]private GameObject uiInventory;

    public override void Awake()
    {
        base.Awake();
       
        uiInventoryScript = uiInventory.GetComponent<Inventory>();
    }
    

    private void OnValidate()
    {
        Utility.TryAssign(ref _player, "Player");
        Utility.TryAssign(ref healthBar, "HealthBar");
        Utility.TryAssign(ref prompt, "Prompt");
        
    }


    private void Start()
    {  
        //Player할당 예외처리코드
        if (_player && _player.PlayerCondition)
        {
            // 초기 healthBar 값 설정
            UpdateHealthBar();
            _player.PlayerCondition.Health.OnChangeValue += UpdateHealthBar;
        }
        else
        {
            Debug.LogError("Player or PlayerCondition is not properly initialized!");
            _player = GameObject.Find("Player")?.GetComponent<Player>();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar && _player)
        {
            healthBar.fillAmount = _player.PlayerCondition.Health.CurrentValue / _player.PlayerCondition.Health.MaxValue;
        }
        else
        {
            Debug.LogError("[UIManager]UpdateHealthBar NullReference!");
            healthBar = GameObject.Find("Health").GetComponent<Image>();
        }
    }

    public void SetPrompt(string commend)
    {
        if (!prompt)
        {
            Debug.LogError("[UIManager]SetPrompt NullReference!");
            prompt = GameObject.Find("Prompt").GetComponent<TextMeshProUGUI>();
        }
        if (!prompt.gameObject.activeInHierarchy)
        {
            prompt.gameObject.SetActive(true);
        }
        prompt.SetText(commend);
    }

    public void ClearPrompt()
    {   if (!prompt)
        {
            Debug.LogError("[UIManager]ClearPrompt NullReference!");
        }
        if (prompt.gameObject.activeInHierarchy)
        {
            prompt.SetText(string.Empty);
            prompt.gameObject.SetActive(false);
        }
    }




    
    
}
