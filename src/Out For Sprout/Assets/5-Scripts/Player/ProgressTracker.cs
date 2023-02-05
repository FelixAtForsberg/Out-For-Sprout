using System;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public static ProgressTracker Instance;
    private float furthestDepth;
    private int furthestLayerIndex;
    private float layerPercentage;
    private float totalProgressPercentage;
    private float timer;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Double progress trackers");
            return;
        }
            
        Instance = this;
    }

    private void Update()
    {
        TrackProgress();
        timer += Time.deltaTime;
    }

    public float GetTimer(){

        return timer;
    }

    private void TrackProgress()
    {
        var players = PlayerTracker.Instance.GetPlayers();
        foreach (var player in players)
        {
            var posY = player.transform.position.y;
            furthestDepth = Mathf.Min(posY, furthestDepth);
        }

        var lastIndex = furthestLayerIndex;
        var data = World.Instance.GetLayerIndexAndProgress(furthestDepth);
        furthestLayerIndex = data.layerIndex;
        layerPercentage = data.layerPercentage;
        totalProgressPercentage = data.fullPercentage;

        if (furthestLayerIndex != lastIndex)
        {
            GameManager.Instance.OnNewLayer.Invoke(furthestLayerIndex);
        }
    }

    public float GetFullProgressPercentage()
    {
        return totalProgressPercentage;
    }
}
