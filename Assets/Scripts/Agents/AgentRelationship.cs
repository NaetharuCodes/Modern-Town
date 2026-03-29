using UnityEngine;

[System.Serializable]
public class AgentRelationship
{
    public string targetAgentId;

    [Range(0, 100)] public float affection;
    [Range(0, 100)] public float respect;
    [Range(0, 100)] public float trust;
    [Range(0, 100)] public float influence;
    [Range(0, 100)] public float compatibility;
    [Range(0, 100)] public float attraction;

    public AgentRelationship(string targetAgentId)
    {
        this.targetAgentId = targetAgentId;
    }

    // Useful composite queries for social systems and events to call against
    public bool IsClose => affection > 70f && trust > 70f;
    public bool IsRomantic => attraction > 60f && affection > 60f;
    public bool IsDeepBond => affection > 70f && trust > 70f && compatibility > 70f;
    public bool IsToxic => attraction > 60f && trust < 30f && respect < 30f;
    public bool IsIntimidated => influence > 70f && affection < 30f;
}
