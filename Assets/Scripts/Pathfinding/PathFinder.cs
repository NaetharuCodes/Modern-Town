using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : MonoBehaviour
{
    public static PathFinder Instance { get; private set; }

    private GridManager _gridManager;

    private static readonly Vector3Int[] Neighbours = {
        new( 0,  1, 0),
        new( 0, -1, 0),
        new(-1,  0, 0),
        new( 1,  0, 0)
    };

    void Awake()
    {
        Instance = this;
        _gridManager = GetComponent<GridManager>();
    }

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int end)
    {
        var openSet = new List<Node>();
        var visited = new Dictionary<Vector3Int, Node>();

        var startNode = new Node(start, null, 0f, Heuristic(start, end));
        openSet.Add(startNode);
        visited[start] = startNode;

        while (openSet.Count > 0)
        {
            var current = PopLowest(openSet);

            if (current.Position == end)
                return ReconstructPath(current);

            foreach (var dir in Neighbours)
            {
                var neighbourPos = current.Position + dir;
                var tileData = GetTileData(neighbourPos);

                if (tileData == null || !tileData.IsPassable)
                    continue;

                float newGCost = current.GCost + tileData.MovementCost;

                if (visited.TryGetValue(neighbourPos, out var existing) && existing.GCost <= newGCost)
                    continue;

                var node = new Node(neighbourPos, current, newGCost, Heuristic(neighbourPos, end));
                visited[neighbourPos] = node;
                openSet.Add(node);
            }
        }

        return null; // no path found
    }

    private TileData GetTileData(Vector3Int pos)
    {
        var tile = _gridManager.GroundFloorTilemap.GetTile(pos);
        if (tile == null) return null;
        var id = _gridManager.TileRegistry.GetId(tile);
        if (id == null) return null;
        return _gridManager.TileRegistry.GetTileData(id);
    }

    private static Node PopLowest(List<Node> list)
    {
        int best = 0;
        for (int i = 1; i < list.Count; i++)
            if (list[i].FCost < list[best].FCost)
                best = i;
        var node = list[best];
        list.RemoveAt(best);
        return node;
    }

    private static float Heuristic(Vector3Int a, Vector3Int b) =>
        Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);

    private static List<Vector3Int> ReconstructPath(Node end)
    {
        var path = new List<Vector3Int>();
        var current = end;
        while (current != null)
        {
            path.Add(current.Position);
            current = current.Parent;
        }
        path.Reverse();
        return path;
    }

    private class Node
    {
        public Vector3Int Position;
        public Node Parent;
        public float GCost;
        public float HCost;
        public float FCost => GCost + HCost;

        public Node(Vector3Int position, Node parent, float gCost, float hCost)
        {
            Position = position;
            Parent = parent;
            GCost = gCost;
            HCost = hCost;
        }
    }
}
