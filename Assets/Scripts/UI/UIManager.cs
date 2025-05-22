using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingleTon<UIManager>   
{
    [SerializeField]private Player _player;
    [SerializeField]private Image healthBar;
    [SerializeField]private TextMeshProUGUI prompt;
    [SerializeField]private GameObject uiInventory;
    public Inventory uiInventoryScript;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI itemValue;
    public override void Awake()
    {
        base.Awake();
        InitInventory();
        uiInventoryScript = uiInventory.GetComponent<Inventory>();
    }

    private void OnValidate()
    {
        Utility.TryAssign(ref _player, "Player");
        Utility.TryAssign(ref healthBar, "HealthBar");
        Utility.TryAssign(ref prompt, "Prompt");
        Utility.TryAssign(ref uiInventory, "Inventory");
        Utility.TryAssign(ref itemName, "ItemName");
        Utility.TryAssign(ref itemDesc, "ItemDescription");
        Utility.TryAssign(ref itemValue, "ItemValue");
        
    }

    public void InitInventory()
    {
        uiInventory.SetActive(false);
        itemName.SetText(string.Empty);
        itemDesc.SetText(string.Empty);
        itemValue.SetText(string.Empty);
    }

    private void Start()
    {
        //Player할당 예외처리코드
        if (_player && _player.PlayerCondition)
        {
            _player.PlayerCondition.OnDamagedAction += UpdateHealthBar;
            // 초기 healthBar 값 설정
            UpdateHealthBar();
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

    public void ShowInventory()
    {
        uiInventory.SetActive(true);
        Inventory inv = uiInventory.GetComponent<Inventory>();
        inv.UpdateInventory();
    }
    
}
