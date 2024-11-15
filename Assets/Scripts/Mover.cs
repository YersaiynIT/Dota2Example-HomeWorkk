using UnityEngine;
using UnityEngine.AI;

public class Mover
{
    private const float MinDistanceToPoint = 0.05f;

    private NavMeshAgent _agent;
    private CharacterView _view;

    private IAnimation _jumpAnimation;
    
    private bool _isWalking;

    public Mover(NavMeshAgent agent, CharacterView view, IAnimation jumpAnimatin)
    {
        _agent = agent;
        _view = view;
        _jumpAnimation = jumpAnimatin;
    }

    public void MoveTo(Vector3 destination)
    {
        if (_agent.CalculatePath(destination, new NavMeshPath()) && _agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            _agent.isStopped = false;
            _isWalking = true;
            _view.StartWalking();
            _agent.SetDestination(destination);
        }
    }

    public void Update()
    {
        if (_isWalking && _agent.remainingDistance <= MinDistanceToPoint)
        {
            StopMoving();
        }

        if (_agent.isOnOffMeshLink)
        {
            _jumpAnimation.Play();
        }
    }
    public void StopMoving()
    {
        _view.StopWalking();
        _agent.isStopped = true;
        _isWalking = false;
    }
}
