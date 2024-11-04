using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDamagable
{
    private const int LeftMouseButton = 0;
    private const float MinDistanceToPoint = 0.05f;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private CharacterView _view;
    [SerializeField] private Health _health;

    private Vector3 _movePointPosition;
    private bool _isWalking;

    private bool _isDead;

    private void Update()
    {
        if (_isDead)
            return;

        ProcessLeftMouse();

        NavMeshPath pathToPoint = new NavMeshPath();

        if (TryGetValidPath(pathToPoint))
        {
            float distanceToPoint = GetPathLength(pathToPoint);

            if (distanceToPoint > MinDistanceToPoint)
            {
                if (_isWalking == false)
                {
                    _view.StartWalking();
                    _agent.isStopped = false;
                    _isWalking = true;
                }

                _agent.SetDestination(_movePointPosition);
            }
            else
            {
                StopMoving();
            }
        }
        else
        {
            StopMoving();
        }
    }

    public void OnDeath()
    {
        _isDead = true;
        _agent.isStopped = true;
    }

    public void TakeDamage(float damage) 
    {
        _health.Reduce(damage);

        _view.TakeDamage();
        _view.CheckHealthStatus();
    }

    private void StopMoving()
    {
        if (_isWalking)
        {
            _view.StopWalking();
            _agent.isStopped = true;
            _isWalking = false;
        }
    }

    private void ProcessLeftMouse()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _movePointPosition = RaycastHelper.GetRaycastHitPoint(ray);

            _view.SetDestinationMarkerTo(_movePointPosition);
        }
    }

    private bool TryGetValidPath(NavMeshPath pathToPoint)
    {
        pathToPoint.ClearCorners();

        if (_agent.CalculatePath(_movePointPosition, pathToPoint) && pathToPoint.status != NavMeshPathStatus.PathInvalid)
            return true;

        return false;
    }

    private float GetPathLength(NavMeshPath pathToPoint)
    {
        float pathLength = 0;

        if (pathToPoint.corners.Length > 1)
            for (int i = 1; i < pathToPoint.corners.Length; i++)
                pathLength += Vector3.Distance(pathToPoint.corners[i - 1], pathToPoint.corners[i]);

        return pathLength;
    }
}
