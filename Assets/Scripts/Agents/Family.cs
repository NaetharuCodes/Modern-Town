using System.Collections.Generic;
using UnityEngine;

public class Family : MonoBehaviour
{
    [Header("Identity")]
    public string familyName;
    public Plot homePlot;

    [Header("Members")]
    public List<Agent> members = new();

    [Header("Finances")]
    public float funds;

    [Header("Traits")]
    public AgentTraits traits = new AgentTraits();

    public void AddFunds(float amount) => funds += amount;

    public bool TrySpend(float amount)
    {
        if (funds < amount) return false;
        funds -= amount;
        return true;
    }
}
