using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField][Range(0, 100)] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCondition playerCondition = other.GetComponent<PlayerCondition>();
            playerCondition.TakeDamage(damage);
        }
    }
}
