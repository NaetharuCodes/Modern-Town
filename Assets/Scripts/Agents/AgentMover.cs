using System.Collections.Generic;
using UnityEngine;

public enum MovementSpeed { Walk, Run }

public class AgentMover : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed  = 6f;

    private Agent _agent;
    private ActionQueue _actionQueue;
    private List<Vector3Int> _currentPath;
    private int _pathIndex;
    private bool _isMoving;
    private float _currentSpeed;

    void Awake()
    {
        _agent = GetComponent<Agent>();
        _actionQueue = GetComponent<ActionQueue>();
        _currentSpeed = walkSpeed;
    }

    public void SetSpeed(MovementSpeed speed) =>
        _currentSpeed = speed == MovementSpeed.Run ? runSpeed : walkSpeed;

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

        if (_currentPath == null)
        {
            Debug.LogWarning($"{_agent.FullName}: Could not find path from {_agent.gridPosition} to {target}.");
            return;
        }

        if (_currentPath.Count <= 1)
        {
            Debug.Log($"{_agent.FullName}: Already at target {target}.");
            return;
        }

        Debug.Log($"{_agent.FullName}: Moving from {_agent.gridPosition} to {target} ({_currentPath.Count - 1} steps).");
        _pathIndex = 1;
        _isMoving = true;
    }

    private void StepAlongPath()
    {
        if (_pathIndex >= _currentPath.Count)
        {
            Debug.Log($"{_agent.FullName}: Arrived at {_agent.gridPosition}.");
            _isMoving = false;
            return;
        }

        var targetWorldPos = GridToWorld(_currentPath[_pathIndex]);
        transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, _currentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWorldPos) < 0.01f)
        {
            transform.position = targetWorldPos;
            _agent.gridPosition = _currentPath[_pathIndex];
            _pathIndex++;
        }
    }

    void OnDrawGizmos()
    {
        if (_currentPath == null || _currentPath.Count < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < _currentPath.Count - 1; i++)
        {
            Gizmos.DrawLine(GridToWorld(_currentPath[i]), GridToWorld(_currentPath[i + 1]));
            Gizmos.DrawSphere(GridToWorld(_currentPath[i]), 0.1f);
        }
        Gizmos.DrawSphere(GridToWorld(_currentPath[^1]), 0.15f);
    }

    private Vector3 GridToWorld(Vector3Int gridPos) =>
        new Vector3(gridPos.x + 0.5f, gridPos.y + 0.5f, transform.position.z);
}
