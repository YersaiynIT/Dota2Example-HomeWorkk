using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class JumpAnimation : IAnimation
{
    private const float JumpDuration = 1;

    private NavMeshAgent _agent;
    private CharacterView _view;

    private AnimationCurve _jumpCurve;
    private Coroutine _jumpCoroutine;
    private AudioSource _jumpSource;

    public JumpAnimation(NavMeshAgent agent, CharacterView view, AnimationCurve jumpCurve, AudioSource jumpSource)
    {
        _agent = agent;
        _view = view;
        _jumpCurve = jumpCurve;
        _jumpSource = jumpSource;
    }

    public void Play()
    {
        if (_jumpCoroutine == null)
        {
            _jumpCoroutine = _view.StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        _view.StartJumping();
        _jumpSource.Play();

        OffMeshLinkData data = _agent.currentOffMeshLinkData;
        Vector3 startPosition = _agent.transform.position;
        Vector3 endPosition = data.endPos + Vector3.up * _agent.baseOffset;

        float progress = 0;

        while (progress < JumpDuration)
        {
            float yOffset = _jumpCurve.Evaluate(progress / JumpDuration);
            _agent.transform.position = Vector3.Lerp(startPosition, endPosition, progress / JumpDuration) + yOffset * Vector3.up;
            _agent.transform.rotation = Quaternion.LookRotation(endPosition - startPosition);
            progress += Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _view.StopJumping();
        _jumpCoroutine = null;
    }
}
