using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("Identity")]
    public string plotName;

    [Header("Boundary")]
    public Vector3Int origin;
    public int width;
    public int height;

    [Header("Ownership")]
    public Family owner;
    public bool forSale;
    public float salePrice;

    public bool IsOwned => owner != null;
    public bool ContainsTile(Vector3Int tile) =>
        tile.x >= origin.x && tile.x < origin.x + width &&
        tile.y >= origin.y && tile.y < origin.y + height;

    void OnDrawGizmos()
    {
        Gizmos.color = IsOwned ? new Color(0f, 1f, 0f, 0.2f) : new Color(1f, 1f, 0f, 0.2f);

        var center = new Vector3(origin.x + width / 2f, origin.y + height / 2f, 0f);
        var size   = new Vector3(width, height, 0f);
        Gizmos.DrawCube(center, size);

        Gizmos.color = IsOwned ? Color.green : Color.yellow;
        Gizmos.DrawWireCube(center, size);
    }
}
