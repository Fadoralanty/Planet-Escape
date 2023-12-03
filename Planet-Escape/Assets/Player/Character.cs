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
    public Action<Buff> OnBuffAdded;
    public Action<Buff> OnBuffUpdated;
    public Action<Buff> OnBuffRemoved;

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
            ActiveBuffs[buff.BuffType].currentStacks += buff.buffStacks;
            OnBuffAdded?.Invoke(ActiveBuffs[buff.BuffType]);  
        }
        else
        {
            Buff newBuff = new Buff(buff);
            ActiveBuffs.Add(newBuff.BuffType, newBuff);
            ActiveBuffs[newBuff.BuffType].currentStacks = newBuff.buffStacks;
            OnBuffAdded?.Invoke(newBuff);
        }
        //display buff
    }

    public void RemoveBuff(Buff buff)
    {
        if (ActiveBuffs.ContainsKey(buff.BuffType))
        {
            ActiveBuffs.Remove(buff.BuffType);
            OnBuffRemoved?.Invoke(buff);
        }
    }

    private void ApplyBuffEffect(Buff buff)
    {
        switch (buff.BuffType)
        {
            case BuffType.Regeneration:
                _damageable.GetHealing(buff.currentStacks);
                buff.currentStacks = buff.currentStacks / 2; 
                break;
            case BuffType.Poison:
                _damageable.TakeDamage(buff.currentStacks);
                buff.currentStacks = buff.currentStacks / 2;
                break;
            case BuffType.Burn:
                _damageable.TakeDamage(buff.currentStacks);
                buff.currentStacks = 0;
                break;
            case BuffType.Ice:
                //_damageable.TakeDamage(buff.currentStacks);
                buff.currentStacks -= 1;
                break;
            case BuffType.Slow:
                break;
            case BuffType.Fast:
                break;
            case BuffType.Stun:
                break;
        }
    }
    

    private void RemoveEmptyBuffs()
    {
        foreach (var buff in ActiveBuffs)
        {
            if (buff.Value.currentStacks == 0)
            {
                RemoveBuff(buff.Value);
                return;
            }
        }
    }
    public void UpdateBuffsAtEndOfTurn()
    {
        if (ActiveBuffs.Count==0) {return; }
        foreach (var buff in ActiveBuffs)
        {
            if (buff.Value.BuffType == BuffType.Regeneration)
            {
                ApplyBuffEffect(buff.Value);
            }
            // ActiveBuffs[buff.Key].buffStacks -= 1;
            //
            // if (ActiveBuffs[buff.Key].buffStacks <= 0) 
            // {
            //     RemoveBuff(buff.Value);
            // }
            
            // display buffs
            OnBuffUpdated?.Invoke(buff.Value);
        }
        RemoveEmptyBuffs();

    }
    public void UpdateBuffsAtBeginningOfTurn()
    {
        if (ActiveBuffs.Count==0) {return; }
        foreach (var buff in ActiveBuffs)
        {

            switch (buff.Value.BuffType)
            {
                case BuffType.Poison:
                case BuffType.Burn:
                    ApplyBuffEffect(buff.Value);
                    break;
            }
            // ActiveBuffs[buff.Key].buffStacks -= 1;
            //
            // if (ActiveBuffs[buff.Key].buffStacks <= 0) 
            // {
            //     RemoveBuff(buff.Value);
            // }
            
            // display buffs
            OnBuffUpdated?.Invoke(buff.Value);
        }
        RemoveEmptyBuffs();
    }
    
    protected virtual void OnDestroy()
    {
        _damageable.OnTakeDamage -= OnTakeDamageHandler;
    }
}
