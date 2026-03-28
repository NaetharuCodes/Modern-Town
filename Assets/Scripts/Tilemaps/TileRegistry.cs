using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileRegistry", menuName = "Modern Town/Tile Registry")]
public class TileRegistry : ScriptableObject
{
    [SerializeField] private List<TileData> entries;

    private Dictionary<string, TileData> _idToData;
    private Dictionary<TileBase, string> _tileToId;

    public void Initialize()
    {
        _idToData = new Dictionary<string, TileData>();
        _tileToId = new Dictionary<TileBase, string>();

        if (entries == null) return;

        foreach (var entry in entries)
        {
            _idToData[entry.id] = entry;
            _tileToId[entry.tile] = entry.id;
        }
    }

    public TileBase GetTile(string id) => _idToData.TryGetValue(id, out var data) ? data.tile : null;
    public TileData GetTileData(string id) => _idToData.TryGetValue(id, out var data) ? data : null;
    public string GetId(TileBase tile) => _tileToId.TryGetValue(tile, out var id) ? id : null;
}
