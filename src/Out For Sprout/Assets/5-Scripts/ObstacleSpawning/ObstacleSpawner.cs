using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] public GameObject obejctToSpawn;
    
    [SerializeField] public List<WorldLayer> layers;
    // Start is called before the first frame update
    void Start()
    {
        SpawnWorld();   
    }

    private void SpawnWorld()
    {
        var startYPosition = 0f;
        foreach(var layer in layers)
        {
            SpawnLayer(layer, startYPosition);
            startYPosition -= layer.spawnLength;
        }
    }

    private void SpawnLayer(WorldLayer layer, float startYPosition)
    {
        var endYPos = startYPosition-layer.spawnLength;
        foreach(var objSettings in layer.obstacleSettings){
            for(int i = 0; i<objSettings.nrOfSpawns;i++){
                    var randomSpawnPos = GetRandomSpawnPosForLayer(startYPosition, endYPos);
                    // TODO check with collision if we can spawn object here safeley


                    Instantiate(objSettings.ObjectToSpawn, randomSpawnPos, Quaternion.identity);
            }
        }
    }

    private Vector2 GetRandomSpawnPosForLayer(float startY, float endY)
    {
        // TODO add game width
        var mapWidth = 10;
        var halfWidth = mapWidth * 0.5f; 
        return new Vector2(Random.Range(-halfWidth, halfWidth), Random.Range(startY, endY));
    }

   /* Vector2 createRandomPosition(){
        Vector2 randomPosition = new Vector2(Random.Range(minPosition.x, maxPosition.y), 
                                            Random.Range(minPosition.x, maxPosition.y));

        return randomPosition;
    }*/

   /* void SpawnObstacle(Vector2 randomPos){
        Instantiate(obejctToSpawn, randomPos, Quaternion.identity);
    }*/

 
}
