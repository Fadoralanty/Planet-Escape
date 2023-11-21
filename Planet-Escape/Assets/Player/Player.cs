using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public PlayerSO playerStats;
    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _damageable.SetData(playerStats.MaxHealth);
    }
}
