using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class MapSaver
{
    [System.Serializable]
    private class TileData
    {
        public int x, y;
        public string id;
    }

    [System.Serializable]
    private class LayerData
    {
        public string name;
        public List<TileData> tiles = new();
    }

    [System.Serializable]
    private class MapData
    {
        public List<LayerData> layers = new();
    }

    [MenuItem("Tools/Modern Town/Save Map")]
    private static void SaveMap()
    {
        var gridManager = Object.FindFirstObjectByType<GridManager>();
        if (gridManager == null)
        {
            Debug.LogError("MapSaver: No GridManager found in scene.");
            return;
        }

        var registry = gridManager.TileRegistry;
        if (registry == null)
        {
            Debug.LogError("MapSaver: GridManager has no TileRegistry assigned.");
            return;
        }

        registry.Initialize();

        var mapData = new MapData();

        mapData.layers.Add(SerializeLayer("Basement", gridManager.BasementTilemap, registry));
        mapData.layers.Add(SerializeLayer("GroundFloor", gridManager.GroundFloorTilemap, registry));
        mapData.layers.Add(SerializeLayer("FirstFloor", gridManager.FirstFloorTilemap, registry));
        mapData.layers.Add(SerializeLayer("SecondFloor", gridManager.SecondFloorTilemap, registry));

        var dir = Path.Combine(Application.streamingAssetsPath, "Maps");
        Directory.CreateDirectory(dir);

        var path = Path.Combine(dir, "default.json");
        File.WriteAllText(path, JsonUtility.ToJson(mapData, prettyPrint: true));
        AssetDatabase.Refresh();

        Debug.Log($"MapSaver: Map saved to {path}");
    }

    private static LayerData SerializeLayer(string name, Tilemap tilemap, TileRegistry registry)
    {
        var layer = new LayerData { name = name };

        tilemap.CompressBounds();
        var bounds = tilemap.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile(pos);
            if (tile == null) continue;

            var id = registry.GetId(tile);
            if (id == null)
            {
                Debug.LogWarning($"MapSaver: Tile at {pos} on layer '{name}' has no registry entry — skipped.");
                continue;
            }

            layer.tiles.Add(new TileData { x = pos.x, y = pos.y, id = id });
        }

        return layer;
    }
}
