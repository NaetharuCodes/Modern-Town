using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AgentTrait
{
    public string key;
    [Range(1, 5)]
    public float value;
}

[System.Serializable]
public class AgentTraits
{
    [SerializeField] private List<AgentTrait> traits = new();

    public bool Has(string key) => traits.Exists(t => t.key == key);

    public float Get(string key)
    {
        var trait = traits.Find(t => t.key == key);
        return trait != null ? trait.value : 0f;
    }

    public void Set(string key, float value)
    {
        var trait = traits.Find(t => t.key == key);
        if (trait != null)
            trait.value = value;
        else
            traits.Add(new AgentTrait { key = key, value = value });
    }

    public void Remove(string key) => traits.RemoveAll(t => t.key == key);
}
