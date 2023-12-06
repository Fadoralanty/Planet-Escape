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
    
    public Dictionary<BuffType, Buff> ActiveBuffs = new Dictionary<BuffType, Buff>();
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
    }

    private void RemoveBuff(Buff buff)
    {
        if (ActiveBuffs.ContainsKey(buff.BuffType))
        {
            ActiveBuffs.Remove(buff.BuffType);
            OnBuffRemoved?.Invoke(buff);
        }
    }   
    private void RemoveBuff(BuffType buff)
    {
        if (ActiveBuffs.ContainsKey(buff))
        {
            OnBuffRemoved?.Invoke(ActiveBuffs[buff]);
            ActiveBuffs.Remove(buff);
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
                buff.currentStacks -= 1;
                break;
            case BuffType.Slow:
                break;
            case BuffType.Fast:
                break;
            case BuffType.Stun:
                break;
            case BuffType.None:
                break;
            case BuffType.Spikes:
                break;
            case BuffType.AtkUp:
                break;
            case BuffType.DefUp:
                break;
        }
    }
    

    private void RemoveEmptyBuffs()
    {
        List<BuffType> buffsToRemove=new List<BuffType>();
        foreach (var buff in ActiveBuffs)
        {
            if (buff.Value.currentStacks == 0)
            {
                //RemoveBuff(buff.Value);
                buffsToRemove.Add(buff.Key);
            }
        }

        foreach (var buff in buffsToRemove)
        {
            RemoveBuff(buff);
        }
    }
    public void UpdateBuffsAtEndOfTurn() //END OF TURN
    {
        if (ActiveBuffs.Count==0) {return; }

        foreach (var buff in ActiveBuffs)
        {
            switch (buff.Value.BuffType)
            {
                case BuffType.Regeneration:
                case BuffType.Burn:
                case BuffType.Ice:
                    ApplyBuffEffect(buff.Value);
                    break;
            }
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
                    ApplyBuffEffect(buff.Value);
                    break;
            }

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
