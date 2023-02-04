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

    public (int, float) GetLayerIndexAndProgress(float checkDepth)
    {
        var layerEndDepth = 0;
        for (int index = 0; index < worldLayers.Count; index++)
        {
            var start = layerEndDepth;
            var layerLength = worldLayers[index].spawnLength;
            layerEndDepth += layerLength;
            if (checkDepth > layerEndDepth)
            {
                var percentage = (start - checkDepth) / layerLength;
                return (index, percentage);
            }
        }

        return (worldLayers.Count - 1, 1.0f);
    }
}
