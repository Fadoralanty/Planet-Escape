using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public abstract class Character : MonoBehaviour
{
    public Damageable Damageable => _damageable;
    [SerializeField] protected Damageable _damageable;
    [SerializeField] protected TextMeshProUGUI DamageIndicatorTMP;
    [SerializeField]protected Animator _animator;
    protected Dictionary<BuffType, Buff> ActiveBuffs = new Dictionary<BuffType, Buff>();

    private void Start()
    {
        _damageable.OnTakeDamage += OnTakeDamageHandler;
        _animator = GetComponent<Animator>();
    }

    protected virtual void OnTakeDamageHandler(float currLife, float damage)
    {
        DamageIndicatorTMP.text = damage.ToString();
        _animator.Play("DamageIndicator");
    }

    public void AddBuff(Buff buff)
    {
        if (ActiveBuffs.ContainsKey(buff.BuffType))
        {
            ActiveBuffs[buff.BuffType].buffStacks += buff.buffStacks;
        }
        else
        {
            ActiveBuffs.Add(buff.BuffType, buff);
        }
        //display buff
    }

    public void RemoveBuff(Buff buff)
    {
        if (ActiveBuffs.ContainsKey(buff.BuffType))
        {
            ActiveBuffs.Remove(buff.BuffType);
        }
    }

    public void UpdateBuffsAtEndOfTurn()
    {
        foreach (var buff in ActiveBuffs)
        {
            ActiveBuffs[buff.Key].buffStacks -= 1;
            
            if (ActiveBuffs[buff.Key].buffStacks <= 0) 
            {
                RemoveBuff(buff.Value);
            }
            // display buffs
        }
        
        
    }

    protected virtual void OnDestroy()
    {
        _damageable.OnTakeDamage -= OnTakeDamageHandler;
    }
}
