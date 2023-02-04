using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]float minNeighbourDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnWorld();   
    }

    private void SpawnWorld()
    {
        var startYPosition = 0;
        var layers = World.Instance.GetWorldLayers();
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

               SpawnObjects(startYPosition, endYPos, objSettings);
                   
            }
        }
    }

    void SpawnObjects(float startYPosition, float endYPos, ObstacleSpawnSettings objSettings){
        var randomSpawnPos = GetRandomSpawnPosForLayer(startYPosition, endYPos);
            // TODO check with collision if we can spawn object here safeley
            
        var safteyCounter=0;
        while(!SpawnCheckOk(objSettings.ObjectToSpawn, randomSpawnPos) && safteyCounter<100){
            safteyCounter++;
            randomSpawnPos = GetRandomSpawnPosForLayer(startYPosition, endYPos);     
                Debug.Log("In while loop");       


        }
        if(safteyCounter>=100){
            Debug.Log("While loop failed");
            return;
        }
        Instantiate(objSettings.ObjectToSpawn, randomSpawnPos, Quaternion.identity);                   

    }



    private Vector2 GetRandomSpawnPosForLayer(float startY, float endY)
    {
        // TODO add game width
        var mapWidth = 10;
        var halfWidth = mapWidth * 0.5f; 
        return new Vector2(Random.Range(-halfWidth, halfWidth), Random.Range(startY, endY));
    }

    private bool SpawnCheckOk(GameObject obstacle, Vector2 spawnpoint){

        var collider = obstacle.GetComponent<CircleCollider2D>();
        
        var magnitudeMax = collider.bounds.max.magnitude;
        var magnitudeMin = collider.bounds.min.magnitude;

        float radie;

        radie = Mathf.Max(magnitudeMax, magnitudeMin);
        
        
        return !Physics2D.OverlapCircle(spawnpoint, radie + minNeighbourDistance);
        
    }


 
}
