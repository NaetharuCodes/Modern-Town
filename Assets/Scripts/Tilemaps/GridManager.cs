using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [Header("Tilemap Layers")]
    [SerializeField] private Tilemap basementTilemap;
    [SerializeField] private Tilemap groundFloorTilemap;
    [SerializeField] private Tilemap firstFloorTilemap;
    [SerializeField] private Tilemap secondFloorTilemap;

    // Update is called once per frame
    void Update()
    {

    }
}
