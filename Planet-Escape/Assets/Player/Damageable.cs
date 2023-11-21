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
    }
    
    public bool IsAlive() => _currentLife > 0;
    public void SetCurrentLife(float life) => _currentLife = life;
    public void SetMaxLife(float life) => maxLife = life;
    public void TakeDamage(float damage)
    {
        if (IsAlive())
        {
            
            _currentBlock -= damage;
            //trigger current block chjange event
            if (_currentBlock > 0)
            {
                return;
            }
            damage = - _currentBlock;
            _currentLife -= damage;
            OnTakeDamage?.Invoke(_currentLife, damage);
            
        }
        else
        {
            OnDie?.Invoke();
            Die();
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
    public void Die()
    {
        //Destroy(gameObject, 3f);
        gameObject.SetActive(false);
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