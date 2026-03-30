using UnityEngine;
using UnityEngine.InputSystem;

public class WorldClickHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Grid grid;
    [SerializeField] private ContextMenuController contextMenu;

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.rightButton.wasPressedThisFrame)
            HandleRightClick(mouse.position.ReadValue());

        if (mouse.leftButton.wasPressedThisFrame)
            HandleLeftClick(mouse.position.ReadValue());
    }

    private void HandleRightClick(Vector2 screenPos)
    {
        var gridPos = ScreenToGrid(screenPos);
        contextMenu.Show(screenPos, gridPos);
    }

    private void HandleLeftClick(Vector2 screenPos)
    {
        // left click selection will be expanded later
    }

    private Vector3Int ScreenToGrid(Vector2 screenPos)
    {
        var worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
        return grid.WorldToCell(worldPos);
    }
}
