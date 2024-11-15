using UnityEngine;
using UnityEngine.AI;


public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private DestinationMarkerManager _markerManager;
    [SerializeField] private InputMovementHandler _inputMovermentHandler;

    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AudioSource _jumpSource;
    private IAnimation _jumpAnimation;

    [SerializeField] private Character _characterPrefab;
    [SerializeField] private Transform _startSpawnPoint;
     
    private NavMeshAgent _agent;
    private CharacterView _view;
    private Mover _mover;
    private Health _health;

    private void Awake()
    {
        Character character = Instantiate(_characterPrefab, _startSpawnPoint.position, Quaternion.identity, null);

        _agent = character.GetComponent<NavMeshAgent>();
        _view = character.GetComponentInChildren<CharacterView>();

        _health = new Health(100);
        _view.Initialize(_health);

        _jumpAnimation = new JumpAnimation(_agent, _view, _jumpCurve, _jumpSource);
        _mover = new Mover(_agent, _view, _jumpAnimation);

        character.Initialize(_view, _mover, _health);

        _inputMovermentHandler.Initialize(character, _markerManager);
        _cameraManager.SetCameraTarget(character.CameraTarget);
    }
}
