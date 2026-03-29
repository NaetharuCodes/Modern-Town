using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Family ActiveFamily { get; private set; }
    public Agent FocusedAgent { get; private set; }

    public event UnityAction<Family> OnFamilyChanged;
    public event UnityAction<Agent> OnFocusedAgentChanged;

    void Awake()
    {
        Instance = this;
    }

    public void SetActiveFamily(Family family)
    {
        ActiveFamily = family;
        OnFamilyChanged?.Invoke(family);
        SetFocusedAgent(family.members[0]);
    }

    public void SetFocusedAgent(Agent agent)
    {
        FocusedAgent = agent;
        OnFocusedAgentChanged?.Invoke(agent);
    }
}
