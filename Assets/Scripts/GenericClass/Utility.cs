using UnityEngine;

public class Utility : MonoBehaviour
{
    public static void TryAssign<T>(ref T field, string name = null) where T : UnityEngine.Object
    {
        if (field == null)
        {
            field = string.IsNullOrEmpty(name) 
                ? FindObjectOfType<T>() 
                : GameObject.Find(name)?.GetComponent<T>();
            if (field == null)
            {
                Debug.LogWarning($"[UIManager] {typeof(T).Name} 할당 실패: {name ?? "자동 탐색"}");
            }
        }
    }
}
