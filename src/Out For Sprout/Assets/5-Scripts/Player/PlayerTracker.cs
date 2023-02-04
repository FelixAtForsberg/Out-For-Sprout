using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker Instance;
    private List<GameObject> playerObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Double singleton");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterPlayer(GameObject player)
    {
        playerObjects.Add(player);
    }

    public void DeregisterPlayer(GameObject player)
    {
        playerObjects.Remove(player);
    }

    public List<GameObject> GetPlayers()
    {
        return playerObjects;
    }
}
