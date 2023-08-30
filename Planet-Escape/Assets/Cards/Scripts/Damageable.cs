using System;
using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public Action<float> OnTakeDamage { get; set; }
    public Action OnDie { get; set; }
    public float MaxLife => maxLife;
    [SerializeField] private float maxLife = 100;
    [SerializeField] private float _currentLife;

    private void Awake()
    {
        SetCurrentLife(maxLife);
    }

    public bool IsAlive() => _currentLife > 0;
    public void SetCurrentLife(float life) => _currentLife = life;
    public void TakeDamage(float damage)
    {
        if (IsAlive())
        {
            _currentLife -= damage;
            OnTakeDamage?.Invoke(_currentLife);
        }
        else
        {
            OnDie?.Invoke();
            Die();
        }
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