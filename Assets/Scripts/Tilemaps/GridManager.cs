using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [Header("Tilemap Layers")]
    [SerializeField] private Tilemap basementTilemap;
    [SerializeField] private Tilemap groundFloorTilemap;
    [SerializeField] private Tilemap firstFloorTilemap;
    [SerializeField] private Tilemap secondFloorTilemap;

    [Header("Config")]
    [SerializeField] private TileRegistry tileRegistry;

    public Tilemap BasementTilemap => basementTilemap;
    public Tilemap GroundFloorTilemap => groundFloorTilemap;
    public Tilemap FirstFloorTilemap => firstFloorTilemap;
    public Tilemap SecondFloorTilemap => secondFloorTilemap;
    public TileRegistry TileRegistry => tileRegistry;
}
