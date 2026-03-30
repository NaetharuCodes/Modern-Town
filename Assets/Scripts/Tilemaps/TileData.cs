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

    [Header("Exits")]
    public bool canExitNorth = true;
    public bool canExitSouth = true;
    public bool canExitEast  = true;
    public bool canExitWest  = true;

    public bool CanExitTo(Vector3Int direction)
    {
        if (direction == Vector3Int.up)    return canExitNorth;
        if (direction == Vector3Int.down)  return canExitSouth;
        if (direction == Vector3Int.right) return canExitEast;
        if (direction == Vector3Int.left)  return canExitWest;
        return true;
    }
}
