using System;
using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public Action<float,float> OnTakeDamage { get; set; }
    public Action OnDie { get; set; }
    public float MaxLife => maxLife;
    [SerializeField] private float maxLife = 100;
    [SerializeField] private float _currentLife;
    [SerializeField] private float _currentBlock;
    
    public void SetData(float maxHealth)
    {
        maxLife = maxHealth;
        SetCurrentLife(maxHealth);
        OnTakeDamage?.Invoke(_currentLife, 0);
    }

    private void Start()
    {
        OnTakeDamage?.Invoke(_currentLife, 0);
    }

    public bool IsAlive() => _currentLife > 0;
    public void SetCurrentLife(float life) => _currentLife = life;
    public void SetMaxLife(float life) => maxLife = life;
    public void TakeDamage(float damage)
    {
            //trigger current block chjange event
        if (_currentBlock > 0)
        {
            _currentBlock -= damage;
            if (_currentBlock > 0)
            {
                OnTakeDamage?.Invoke(_currentLife , 0);
                return;
            }
            else
            {
                damage = - _currentBlock;
            }
        }
        _currentLife -= damage;
        OnTakeDamage?.Invoke(_currentLife, damage);
        if (!IsAlive())
        {
            OnDie?.Invoke();
        }
    }

    public void GainBlock(float block)
    {
        _currentBlock += block;
    }

    public void RemoveBlock()
    {
        _currentBlock = 0;
    }

    private void OnDestroy()
    {
        Dispose();
    }

    public void Dispose()
    {
        OnTakeDamage -= OnTakeDamage;
        OnDie -= OnDie;
    }
}