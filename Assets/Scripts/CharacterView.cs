using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int IsWalkingKey = Animator.StringToHash("IsWalking");
    private readonly int IsJumpingKey = Animator.StringToHash("IsJumping");
    private readonly int TakeDamageKey = Animator.StringToHash("TakeDamage");
    private readonly int IsDeadKey = Animator.StringToHash("IsDead");

    [SerializeField] private Animator _animator;

    private const int InjuredLayerIndex = 1;
    private const float InjuryThreshold = 0.3f;
    private bool _isInjured;

    private Health _health;

    public void Initialize(Health health)
    {
        _health = health;
    }

    public void CheckHealthStatus()
    {
        float currentHealthPercentage = _health.CurrentValue / _health.MaxValue;

        if (currentHealthPercentage < InjuryThreshold)
            _isInjured = true;
        else
            _isInjured = false;

        if (_isInjured)
            _animator.SetLayerWeight(InjuredLayerIndex, 1);
        else
            _animator.SetLayerWeight(InjuredLayerIndex, 0);
    }

    public void StartWalking() => _animator.SetBool(IsWalkingKey, true);

    public void StopWalking() => _animator.SetBool(IsWalkingKey, false);

    public void StartJumping() => _animator.SetBool(IsJumpingKey, true);

    public void StopJumping() => _animator.SetBool(IsJumpingKey, false);

    public void TakeDamage() => _animator.SetTrigger(TakeDamageKey);

    public void Die()
    {
        _animator.SetBool(IsDeadKey, true);
    }

}
