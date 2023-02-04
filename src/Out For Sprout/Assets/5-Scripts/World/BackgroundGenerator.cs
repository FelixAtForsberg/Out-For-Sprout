using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap worldTilemap;

    private void Start()
    {
        var layers = World.Instance.GetWorldLayers();
        var layerStartY = 0;
        foreach (var layer in layers)
        {
            layerStartY += layer.spawnLength;
            SetTilesForLayer(layer, layerStartY);
        }
    }

    private void SetTilesForLayer(WorldLayer worldLayer, int gridStartY)
    {
        var tile = worldLayer.backgroundTile;
        int mapWidthRows = 10;
        var halfWidthRows = mapWidthRows / 2;

        var gridEnd = gridStartY + worldLayer.spawnLength;
        for (int x = -halfWidthRows; x <= halfWidthRows; x++)
        {
            for (int y = gridStartY; y < gridEnd; y++)
            {
                worldTilemap.SetTile(new Vector3Int(x, y, 0),tile);
            }
        }
    }
}
