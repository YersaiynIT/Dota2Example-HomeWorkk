using UnityEngine;

public class Health
{
    private float _maxValue;
    private float _currentValue;

    public float MaxValue => _maxValue;
    public float CurrentValue => _currentValue;

    public Health(int maxValue)
    {
        _maxValue = maxValue;

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
