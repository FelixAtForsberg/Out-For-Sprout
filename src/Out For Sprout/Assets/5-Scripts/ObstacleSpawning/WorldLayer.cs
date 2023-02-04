using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(WorldLayer), fileName = nameof(WorldLayer))]

public class WorldLayer : ScriptableObject
{
    public float spawnLength;   
    [SerializeField] public Vector2 minPosition;
    [SerializeField] public Vector2 maxPosition;

    public List<ObstacleSpawnSettings> obstacleSettings;
    
    
}
