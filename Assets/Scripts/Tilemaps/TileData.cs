using UnityEngine;
using UnityEngine.Tilemaps;

public enum TilePassability
{
    Ideal,
    Difficult,
    VeryDifficult,
    Water,
    Impassable
}

[CreateAssetMenu(fileName = "TileData", menuName = "Modern Town/Tile Data")]
public class TileData : ScriptableObject
{
    [Header("Identity")]
    public string id;
    public string displayName;
    public TileBase tile;

    [Header("Movement")]
    public TilePassability passability;

    public float MovementCost => passability switch
    {
        TilePassability.Ideal         => 1f,
        TilePassability.Difficult     => 2f,
        TilePassability.VeryDifficult => 4f,
        TilePassability.Water         => 6f,
        TilePassability.Impassable    => float.MaxValue,
        _                             => 1f
    };

    public bool IsPassable => passability != TilePassability.Impassable;
}
