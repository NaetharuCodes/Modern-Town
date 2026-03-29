using UnityEngine;

public class AgentMoveTest : MonoBehaviour
{
    void Start()
    {
        GetComponent<ActionQueue>().Enqueue(new AgentAction
        {
            type = ActionType.MoveTo,
            targetPosition = new Vector3Int(10, 5, 0)
        });
    }
}