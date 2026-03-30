using UnityEngine;
using UnityEngine.UIElements;

public class ContextMenuController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private VisualElement _menu;
    private Vector3Int _targetGridPos;

    void Start()
    {
        _menu = new VisualElement();
        _menu.name = "context-menu";
        _menu.AddToClassList("context-menu");
        _menu.style.display = DisplayStyle.None;
        uiDocument.rootVisualElement.Add(_menu);
    }

    public void Show(Vector2 screenPos, Vector3Int gridPos)
    {
        _targetGridPos = gridPos;
        _menu.Clear();

        // convert screen pos to UI panel pos (UI Toolkit y-axis is flipped)
        var panelPos = RuntimePanelUtils.ScreenToPanel(
            uiDocument.rootVisualElement.panel,
            new Vector2(screenPos.x, Screen.height - screenPos.y)
        );

        _menu.style.left = panelPos.x;
        _menu.style.top  = panelPos.y;

        AddOption("Walk", () => SendMoveAction(gridPos, walk: true));
        AddOption("Run",  () => SendMoveAction(gridPos, walk: false));

        _menu.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        _menu.style.display = DisplayStyle.None;
    }

    private void AddOption(string label, System.Action onSelect)
    {
        var btn = new Button(() => { onSelect(); Hide(); });
        btn.text = label;
        btn.AddToClassList("context-menu-item");
        _menu.Add(btn);
    }

    private void SendMoveAction(Vector3Int gridPos, bool walk)
    {
        var agent = GameManager.Instance.FocusedAgent;
        Debug.Log($"FocusedAgent: {agent?.FullName ?? "NULL"}");
        if (agent == null) return;

        var mover = agent.GetComponent<AgentMover>();
        Debug.Log($"AgentMover: {(mover == null ? "NULL" : "found")}");
        if (mover == null) return;

        Debug.Log($"Sending MoveTo: {gridPos}");
        mover.SetSpeed(walk ? MovementSpeed.Walk : MovementSpeed.Run);

        agent.GetComponent<ActionQueue>().Enqueue(new AgentAction {
            type = ActionType.MoveTo,
            targetPosition = gridPos
        });
    }
}
