using System;
using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    public Action<float,float> OnTakeDamage { get; set; }
    public Action<float> OnBlockChange { get; set; }
    public Action OnDie { get; set; }
    public float MaxLife => maxLife;
    [SerializeField] private float maxLife = 100;
    public float CurrentLife => _currentLife;
    [SerializeField] private float _currentLife;
    
    public float CurrentBlock => _currentBlock;
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
            OnBlockChange?.Invoke(_currentBlock);
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

    public void GetHealing(float Heal)
    {
        if (!IsAlive()) { return; }
        _currentLife += Heal;
        if (_currentLife > maxLife)
        {
            _currentLife = maxLife;
        }
        OnTakeDamage?.Invoke(_currentLife, Heal);
    }
    public void GainBlock(float block)
    {
        _currentBlock += block;
        OnBlockChange?.Invoke(_currentBlock);
    }

    public void RemoveBlock()
    {
        _currentBlock = 0;
        OnBlockChange?.Invoke(_currentBlock);
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