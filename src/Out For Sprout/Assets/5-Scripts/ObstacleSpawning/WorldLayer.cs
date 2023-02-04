using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = nameof(WorldLayer), fileName = nameof(WorldLayer))]

public class WorldLayer : ScriptableObject
{
    public int spawnLength;   
    public List<ObstacleSpawnSettings> obstacleSettings;
    public Tile backgroundTile;
}
