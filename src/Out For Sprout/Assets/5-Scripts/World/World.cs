using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance;
    [SerializeField] private List<WorldLayer> worldLayers;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Double world in scene");
            return;
        }
        Instance = this;
    }

    public List<WorldLayer> GetWorldLayers()
    {
        return worldLayers;
    }
}
