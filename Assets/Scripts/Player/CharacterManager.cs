

using UnityEngine;

public class CharacterManager : SingleTon<CharacterManager>
{
    public Player Player { get; set; }

    private void Start()
    {
        if (!Player)
        {
            Debug.LogError("Player is not set");
        }
    }
}
