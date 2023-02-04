using System;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public static ProgressTracker Instance;
    private float furthestDepth;
    private int furthestLayerIndex;
    private float layerPercentage;

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
    }

    private void TrackProgress()
    {
        var players = PlayerTracker.Instance.GetPlayers();
        foreach (var player in players)
        {
            var posY = player.transform.position.y;
            furthestDepth = Mathf.Min(posY, furthestDepth);
        }

        (furthestLayerIndex, layerPercentage) = World.Instance.GetLayerIndexAndProgress(furthestDepth);
    }
}
