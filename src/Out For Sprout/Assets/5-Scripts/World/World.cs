using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance;
    [SerializeField] private List<WorldLayer> worldLayers;
    private float fullLength;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Double world in scene");
            return;
        }
        Instance = this;
        fullLength = CalculateFullLength();
    }

    private float CalculateFullLength()
    {
        var length = 0;
        foreach (var layer in worldLayers)
        {
            length += layer.spawnLength;
        }

        return length;
    }

    public List<WorldLayer> GetWorldLayers()
    {
        return worldLayers;
    }

    public struct ProgressData
    {
        public int layerIndex;
        public float layerPercentage;
        public float fullPercentage;
    }
    
    public ProgressData GetLayerIndexAndProgress(float checkDepth)
    {
        var layerEndDepth = 0;
        for (int index = 0; index < worldLayers.Count; index++)
        {
            var start = layerEndDepth;
            var layerLength = worldLayers[index].spawnLength;
            layerEndDepth -= layerLength;
            if (checkDepth > layerEndDepth)
            {
                var percentage = (start - checkDepth) / layerLength;
                var totalPercentage = checkDepth / -fullLength;
                return new ProgressData()
                {
                    layerIndex = index,
                    layerPercentage = percentage,
                    fullPercentage = totalPercentage
                };
            }
        }

        return new ProgressData(){
            layerIndex = worldLayers.Count - 1, 
            layerPercentage = 1.0f,
            fullPercentage = 1.0f
        };
    }
}
