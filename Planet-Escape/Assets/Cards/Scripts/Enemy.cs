using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemySO _enemySo;
    public Damageable Damageable => _damageable;
    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _damageable.SetData(_enemySo.MaxHealth);
    }
}
