using System.Collections.Generic;

[System.Serializable]
public class AgentRelationships
{
    private Dictionary<string, AgentRelationship> _relationships = new();

    public AgentRelationship Get(string targetAgentId)
    {
        if (!_relationships.TryGetValue(targetAgentId, out var rel))
        {
            rel = new AgentRelationship(targetAgentId);
            _relationships[targetAgentId] = rel;
        }
        return rel;
    }

    public bool Has(string targetAgentId) => _relationships.ContainsKey(targetAgentId);

    public IEnumerable<AgentRelationship> All => _relationships.Values;
}
