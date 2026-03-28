[System.Serializable]
public class AgentNeeds
{
    public float hunger;
    public float sleep;
    public float hygiene;
    public float social;
    public float fun;
    public float comfort;
    public float bladder;

    // Returns true if any need is critically low (under 10)
    public bool HasCriticalNeed =>
        hunger < 10f || sleep < 10f || hygiene < 10f ||
        social < 10f || fun < 10f || comfort < 10f || bladder < 10f;
}
