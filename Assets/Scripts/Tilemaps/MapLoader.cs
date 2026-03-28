using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapLoader : MonoBehaviour
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
        public List<TileData> tiles;
    }

    [System.Serializable]
    private class MapData
    {
        public List<LayerData> layers;
    }

    private GridManager _gridManager;
    private Dictionary<string, Tilemap> _layerMap;

    void Awake()
    {
        _gridManager = GetComponent<GridManager>();

        _layerMap = new Dictionary<string, Tilemap>
        {
            { "Basement",    _gridManager.BasementTilemap },
            { "GroundFloor", _gridManager.GroundFloorTilemap },
            { "FirstFloor",  _gridManager.FirstFloorTilemap },
            { "SecondFloor", _gridManager.SecondFloorTilemap }
        };

        LoadMap("default");
    }

    private void LoadMap(string mapName)
    {
        var path = Path.Combine(Application.streamingAssetsPath, "Maps", $"{mapName}.json");

        if (!File.Exists(path))
        {
            Debug.LogError($"MapLoader: Map file not found at {path}");
            return;
        }

        var mapData = JsonUtility.FromJson<MapData>(File.ReadAllText(path));
        var registry = _gridManager.TileRegistry;
        registry.Initialize();

        foreach (var layer in mapData.layers)
        {
            if (!_layerMap.TryGetValue(layer.name, out var tilemap))
            {
                Debug.LogWarning($"MapLoader: No tilemap found for layer '{layer.name}' — skipped.");
                continue;
            }

            tilemap.ClearAllTiles();

            foreach (var tileData in layer.tiles)
            {
                var tile = registry.GetTile(tileData.id);
                if (tile == null)
                {
                    Debug.LogWarning($"MapLoader: Unknown tile id '{tileData.id}' — skipped.");
                    continue;
                }

                tilemap.SetTile(new Vector3Int(tileData.x, tileData.y, 0), tile);
            }
        }
    }
}
