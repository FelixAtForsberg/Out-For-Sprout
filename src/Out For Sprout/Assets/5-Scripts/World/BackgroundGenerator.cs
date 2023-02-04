using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap worldTilemap;
    [SerializeField] private GameObject endScreenObject;

    private void Start()
    {
        var cellSizeY = worldTilemap.layoutGrid.cellSize.y;
        
        var layers = World.Instance.GetWorldLayers();
        var layerStartY = 0;
        foreach (var layer in layers)
        {
            SetTilesForLayer(layer, layerStartY, cellSizeY);
            layerStartY -= layer.spawnLength;
        }

        endScreenObject.transform.position = new Vector3(0, layerStartY, 0);
    }

    private void SetTilesForLayer(WorldLayer worldLayer, int startY, float cellSizeY)
    {
        var tile = worldLayer.backgroundTile;
        int mapWidthSize = 10;
        var halfWidthRows = Mathf.FloorToInt(mapWidthSize / (2f*cellSizeY));
        
        var endY = startY - worldLayer.spawnLength;

        var gridStartY = Mathf.FloorToInt(startY / cellSizeY);
        var gridEndY = Mathf.FloorToInt(endY / cellSizeY);
        for (int x = -halfWidthRows; x <= halfWidthRows; x++)
        {
            for (int y = gridStartY; y > gridEndY; y--)
            {
                worldTilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
}
