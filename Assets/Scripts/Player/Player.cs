using UnityEngine;

public class Player : MonoBehaviour
{   
    private PlayerController _playerController;
    private PlayerCondition _playerCondition;
    public PlayerCondition PlayerCondition
    {
        get=>_playerCondition;
        private set=>_playerCondition = value;
    }
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        _playerController = GetComponent<PlayerController>();
        _playerCondition = GetComponent<PlayerCondition>();

    }


}
