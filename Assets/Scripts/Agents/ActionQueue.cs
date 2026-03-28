using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    MoveTo,
    // Interact, // stub for later
    // Wait,     // stub for later
}

[System.Serializable]
public class AgentAction
{
    public ActionType type;
    public Vector3Int targetPosition;
}

public class ActionQueue : MonoBehaviour
{
    private readonly Queue<AgentAction> _queue = new();

    public bool HasActions => _queue.Count > 0;

    public void Enqueue(AgentAction action) => _queue.Enqueue(action);

    public AgentAction Dequeue() => _queue.Dequeue();

    public AgentAction Peek() => _queue.Peek();

    public void Clear() => _queue.Clear();
}
