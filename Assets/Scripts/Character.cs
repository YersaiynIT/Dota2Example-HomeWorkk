using UnityEngine;

public class Character : MonoBehaviour, IDamagable
{
    [SerializeField] private Transform _cameraTarget;
    public Transform CameraTarget => _cameraTarget;

    private CharacterView _view;
    private Mover _mover;
    private Health _health;

    private bool _isDead;

    public void Initialize(CharacterView view, Mover mover, Health health)
    {
        _view = view;
        _mover = mover;
        _health = health;
    }

    private void Update()
    {
        if (_isDead)
            return;

        _mover.Update();
    }

    public void ProcessMoveTo(Vector3 destination)
    {
        if (_isDead)
            return;

        _mover.MoveTo(destination);
    }

    public void OnDeath()
    {
        _isDead = true;
        _view.Die();
    }

    public void TakeDamage(float damage) 
    {
        _health.Reduce(damage);

        _view.TakeDamage();
        _view.CheckHealthStatus();

        if (_health.CurrentValue <= 0)
        {
            OnDeath();
        }
    }
    

}
