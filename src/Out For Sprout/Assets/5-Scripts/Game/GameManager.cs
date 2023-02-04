using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent OnPlayerDeath = new UnityEvent();
    public UnityEvent OnPlayerWin = new UnityEvent();
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Duplicate " + nameof(GameManager));
            return;
        }
        Instance = this;
    }

    public void TriggerPlayerDeath()
    {
        OnPlayerDeath.Invoke();
        Debug.LogError("You died");
    }

    public void TriggerVictory()
    {
        OnPlayerWin.Invoke();
        Debug.LogError("Victory");
    }
}
