using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BottomPanelController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private VisualElement _root;
    private Label _agentName;
    private Label _agentMeta;
    private VisualElement _contentStats;

    private readonly string[] _tabs = { "stats", "inventory", "career", "social", "build", "catalogue" };
    private string _activeTab = "stats";

    private readonly (string field, string label)[] _needs = {
        ("hunger",      "Hunger"),
        ("energy",      "Energy"),
        ("bladder",     "Bladder"),
        ("social",      "Social"),
        ("fun",         "Fun"),
        ("hygiene",     "Hygiene"),
        ("comfort",     "Comfort"),
        ("environment", "Environment")
    };

    private Dictionary<string, VisualElement> _needFills = new();

    void Start()
    {
        _root = uiDocument.rootVisualElement;

        _agentName = _root.Q<Label>("agent-name");
        _agentMeta = _root.Q<Label>("agent-meta");
        _contentStats = _root.Q("content-stats");

        SetupTabs();
        BuildNeedBars();
        ShowTab("stats");

        GameManager.Instance.OnFocusedAgentChanged += RefreshAgent;

        if (GameManager.Instance.FocusedAgent != null)
            RefreshAgent(GameManager.Instance.FocusedAgent);
    }

    void OnDestroy()
    {
        GameManager.Instance.OnFocusedAgentChanged -= RefreshAgent;
    }

    void Update()
    {
        if (GameManager.Instance.FocusedAgent != null && _activeTab == "stats")
            UpdateNeedBars(GameManager.Instance.FocusedAgent.needs);
    }

    private void SetupTabs()
    {
        foreach (var tab in _tabs)
        {
            var btn = _root.Q<Button>($"tab-{tab}");
            var tabName = tab;
            btn.RegisterCallback<ClickEvent>(_ => ShowTab(tabName));
        }
    }

    private void ShowTab(string tab)
    {
        foreach (var t in _tabs)
        {
            _root.Q($"content-{t}").RemoveFromClassList("content--visible");
            _root.Q<Button>($"tab-{t}").RemoveFromClassList("tab--active");
        }

        _root.Q($"content-{tab}").AddToClassList("content--visible");
        _root.Q<Button>($"tab-{tab}").AddToClassList("tab--active");
        _activeTab = tab;
    }

    private void BuildNeedBars()
    {
        _needFills.Clear();
        _contentStats.Clear();

        foreach (var (field, label) in _needs)
        {
            var row = new VisualElement();
            row.AddToClassList("need-row");

            var lbl = new Label(label);
            lbl.AddToClassList("need-label");

            var barBg = new VisualElement();
            barBg.AddToClassList("need-bar-bg");

            var fill = new VisualElement();
            fill.AddToClassList("need-bar-fill");
            fill.style.width = Length.Percent(100);
            barBg.Add(fill);

            var val = new Label("100");
            val.AddToClassList("need-value");
            val.name = $"need-val-{field}";

            row.Add(lbl);
            row.Add(barBg);
            row.Add(val);
            _contentStats.Add(row);

            _needFills[field] = fill;
        }
    }

    private void RefreshAgent(Agent agent)
    {
        _agentName.text = agent.FullName;
        _agentMeta.text = $"Age {agent.age} · {agent.agentType}";
        UpdateNeedBars(agent.needs);
    }

    private void UpdateNeedBars(AgentNeeds needs)
    {
        UpdateBar("hunger",      needs.hunger);
        UpdateBar("energy",      needs.energy);
        UpdateBar("bladder",     needs.bladder);
        UpdateBar("social",      needs.social);
        UpdateBar("fun",         needs.fun);
        UpdateBar("hygiene",     needs.hygiene);
        UpdateBar("comfort",     needs.comfort);
        UpdateBar("environment", needs.environment);
    }

    private void UpdateBar(string field, float value)
    {
        if (!_needFills.TryGetValue(field, out var fill)) return;

        fill.style.width = Length.Percent(value);
        fill.RemoveFromClassList("need-bar-fill--low");
        fill.RemoveFromClassList("need-bar-fill--critical");

        if (value < 15f)
            fill.AddToClassList("need-bar-fill--critical");
        else if (value < 35f)
            fill.AddToClassList("need-bar-fill--low");

        var valLabel = _root.Q<Label>($"need-val-{field}");
        if (valLabel != null)
            valLabel.text = Mathf.RoundToInt(value).ToString();
    }
}
