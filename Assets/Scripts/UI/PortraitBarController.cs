using UnityEngine;
using UnityEngine.UIElements;

public class PortraitBarController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private VisualElement _portraitBar;

    void Start()
    {
        _portraitBar = uiDocument.rootVisualElement.Q("portrait-bar");

        GameManager.Instance.OnFamilyChanged += RebuildPortraits;
        GameManager.Instance.OnFocusedAgentChanged += UpdateFocusedPortrait;

        if (GameManager.Instance.ActiveFamily != null)
            RebuildPortraits(GameManager.Instance.ActiveFamily);
    }

    void OnDestroy()
    {
        GameManager.Instance.OnFamilyChanged -= RebuildPortraits;
        GameManager.Instance.OnFocusedAgentChanged -= UpdateFocusedPortrait;
    }

    private void RebuildPortraits(Family family)
    {
        _portraitBar.Clear();

        foreach (var agent in family.members)
        {
            var portrait = CreatePortrait(agent);
            _portraitBar.Add(portrait);
        }
    }

    private VisualElement CreatePortrait(Agent agent)
    {
        var button = new VisualElement();
        button.AddToClassList("portrait-button");

        var initials = new Label($"{agent.firstName[0]}{agent.lastName[0]}");
        initials.AddToClassList("portrait-initials");
        button.Add(initials);

        var name = new Label(agent.firstName);
        name.AddToClassList("portrait-name");
        button.Add(name);

        button.RegisterCallback<ClickEvent>(_ => GameManager.Instance.SetFocusedAgent(agent));

        return button;
    }

    private void UpdateFocusedPortrait(Agent focusedAgent)
    {
        var family = GameManager.Instance.ActiveFamily;
        if (family == null) return;

        for (int i = 0; i < family.members.Count; i++)
        {
            var portrait = _portraitBar[i];
            if (family.members[i] == focusedAgent)
                portrait.AddToClassList("portrait-button--focused");
            else
                portrait.RemoveFromClassList("portrait-button--focused");
        }
    }
}
