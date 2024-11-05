using Cinemachine;
using UnityEngine;
using UnityEngine.AI;


public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private DestinationMarkerManager _markerManager;

    [SerializeField] private Character _characterPrefab;
    [SerializeField] private Transform _startPoint;
     
    private NavMeshAgent _agent;
    private CharacterView _view;
    private Mover _mover;
    private Health _health;

    private void Awake()
    {
        Character character = Instantiate(_characterPrefab, _startPoint.position, Quaternion.identity, null);

        _agent = character.GetComponent<NavMeshAgent>();
        _view = character.GetComponentInChildren<CharacterView>();

        _mover = new Mover(_agent, _view);
        _health = new Health(100);

        character.Initialize(_view, _mover, _health);
        
        _view.Initialize(_health);
        _inputHandler.Initialize(character, _markerManager);

        _camera.Follow = character.CameraTarget;
        _camera.LookAt = character.CameraTarget;
    }
}
