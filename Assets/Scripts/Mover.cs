using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover
{
    private const float MinDistanceToPoint = 0.05f;

    private NavMeshAgent _agent;
    private CharacterView _view;

    private bool _isWalking;

    public Mover(NavMeshAgent agent, CharacterView view)
    {
        _agent = agent;
        _view = view;
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
    }

    public void StopMoving()
    {
        _view.StopWalking();
        _agent.isStopped = true;
        _isWalking = false;
    }
}
