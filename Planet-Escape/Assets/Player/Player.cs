using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public PlayerSO playerStats;
    public Animator PlayerSpriteAnimator;
    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _damageable.OnDie += OnDieHandler;
    }

    private void OnDieHandler()
    {
        PlayerSpriteAnimator.Play("Death");
    }

    protected override void OnTakeDamageHandler(float currLife, float damage)
    {
        base.OnTakeDamageHandler(currLife, damage);
        PlayerSpriteAnimator.Play("Hurt");

    }
}
