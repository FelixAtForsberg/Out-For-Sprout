using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        if (PlayerTracker.Instance != null)
        {
            PlayerTracker.Instance.RegisterPlayer(gameObject);
        }
    }

    private void OnDisable()
    {
        if (PlayerTracker.Instance != null)
        {
            PlayerTracker.Instance.DeregisterPlayer(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<DeathTrigger>())
        {
            Destroy(gameObject);
            GameManager.Instance.TriggerPlayerDeath();
        }
    }
}
