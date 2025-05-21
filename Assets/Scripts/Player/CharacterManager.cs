

using UnityEngine;

public class CharacterManager : SingleTon<CharacterManager>
{
    public Player Player { get; set; }

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (!Player)
        {
            Debug.LogError("Player is not set");
        }
    }
}
