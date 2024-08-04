using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private float _maxHealth = 1f;
    [SerializeField] private float _value;
    private bool isInvincible = false;

    public event Action Died;
    public event Action<float> Changed;

    public float Value => _value;
    public float MaxHealth => _maxHealth;

    
    public bool IsInvincible
    {
        get
        {
            { return isInvincible; }
        }
        set
        {
            isInvincible = value;
        }
    }

    private void Start()
    {
        _value = _maxHealth;
    }

    private void Die()
    {
        
        _value = 0;
        Changed?.Invoke(_value);
        Died?.Invoke();
        
    }

    public void Decrease(float value)
    {
        if (isInvincible)
        {
            return;
        }
        _value -= value;
        if (_value <= 0)
        {
            Die();
            return;
        }
        Changed?.Invoke(_value);
    }

    public void PermanentDeath()
    {
        _value = 0;
        Die();
    }
}
