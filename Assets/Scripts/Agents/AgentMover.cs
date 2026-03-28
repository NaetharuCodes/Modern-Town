using System.Collections.Generic;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Agent _agent;
    private ActionQueue _actionQueue;
    private List<Vector3Int> _currentPath;
    private int _pathIndex;
    private bool _isMoving;

    void Awake()
    {
        _agent = GetComponent<Agent>();
        _actionQueue = GetComponent<ActionQueue>();
    }

    void Update()
    {
        if (_isMoving)
        {
            StepAlongPath();
            return;
        }

        if (_actionQueue.HasActions && _actionQueue.Peek().type == ActionType.MoveTo)
        {
            var action = _actionQueue.Dequeue();
            StartMovement(action.targetPosition);
        }
    }

    private void StartMovement(Vector3Int target)
    {
        _currentPath = PathFinder.Instance.FindPath(_agent.gridPosition, target);

        if (_currentPath == null || _currentPath.Count <= 1)
            return;

        _pathIndex = 1; // index 0 is the current position
        _isMoving = true;
    }

    private void StepAlongPath()
    {
        if (_pathIndex >= _currentPath.Count)
        {
            _isMoving = false;
            return;
        }

        var targetWorldPos = GridToWorld(_currentPath[_pathIndex]);
        transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWorldPos) < 0.01f)
        {
            transform.position = targetWorldPos;
            _agent.gridPosition = _currentPath[_pathIndex];
            _pathIndex++;
        }
    }

    private Vector3 GridToWorld(Vector3Int gridPos)
    {
        // Tilemap cell centres sit at (x + 0.5, y + 0.5) in world space by default
        return new Vector3(gridPos.x + 0.5f, gridPos.y + 0.5f, transform.position.z);
    }
}
