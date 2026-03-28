using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileRegistry", menuName = "Modern Town/Tile Registry")]
public class TileRegistry : ScriptableObject
{
    [System.Serializable]
    public class TileEntry
    {
        public string id;
        public TileBase tile;
    }

    [SerializeField] private List<TileEntry> entries;

    private Dictionary<string, TileBase> _idToTile;
    private Dictionary<TileBase, string> _tileToId;

    public void Initialize()
    {
        _idToTile = new Dictionary<string, TileBase>();
        _tileToId = new Dictionary<TileBase, string>();

        foreach (var entry in entries)
        {
            _idToTile[entry.id] = entry.tile;
            _tileToId[entry.tile] = entry.id;
        }
    }

    public TileBase GetTile(string id) => _idToTile.TryGetValue(id, out var tile) ? tile : null;
    public string GetId(TileBase tile) => _tileToId.TryGetValue(tile, out var id) ? id : null;
}
