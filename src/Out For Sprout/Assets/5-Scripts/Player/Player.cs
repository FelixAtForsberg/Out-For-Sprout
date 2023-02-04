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
}
