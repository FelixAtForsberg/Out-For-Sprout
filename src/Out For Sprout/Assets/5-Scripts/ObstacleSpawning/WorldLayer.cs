using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(WorldLayer), fileName = nameof(WorldLayer))]

public class WorldLayer : ScriptableObject
{
    public float spawnLength;   
    public List<ObstacleSpawnSettings> obstacleSettings;
    
    
}
