using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingleTon<UIManager>   
{
    [SerializeField]private Player _player;
    [SerializeField]private Image healthBar;
    [SerializeField]private TextMeshProUGUI prompt;

    public override void Awake()
    {
        base.Awake();
        _player = CharacterManager.Instance.Player;
        

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
