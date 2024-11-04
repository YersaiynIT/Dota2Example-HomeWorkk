using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;
    private float _currentValue;

    public float MaxValue => _maxValue;
    public float CurrentValue => _currentValue;

    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void Reduce(float value)
    {
        if (value < 0)
        {
            Debug.LogError("value < 0");
            return;
        }

        _currentValue -= value;

        if (_currentValue <= 0)
        {
            _currentValue = 0;
            Debug.Log("Я умер!");
            return;
        }

        Debug.Log($"Текущее здоровье: {_currentValue}");
    }
}
