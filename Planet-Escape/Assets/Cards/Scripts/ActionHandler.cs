    using System;
    using System.Collections;
using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    public void DoActionMultiple(Card_SO.CardAction action, List<Enemy> targets, Character owner)
    {
        foreach (var target in targets)
        {
            DoActionSingle(action, target, owner);
        }
    }
    public void DoActionSingle(Card_SO.CardAction action, Character target, Character owner)
    {
        switch (action.actionType)
        {
            case ActionType.DealDamage:
                DealDamage(action.amount, target.Damageable);
                if (target.ActiveBuffs.ContainsKey(BuffType.Spikes))
                {
                    DealDamage(target.ActiveBuffs[BuffType.Spikes].buffStacks, owner.Damageable);
                }
                break;
            case ActionType.GainBlock:
                GainBlock(action.amount, target.Damageable);
                break;
            case ActionType.DrawCards:
                BattleManager.Singleton.DrawCards(action.amount);
                break;
            case ActionType.Heal:
                break;
            case ActionType.ApplyBuff:
                target.AddBuff(action.Buff);
                break;
        }
    }
    
    private void DealDamage(int amount, Damageable target)
    {
        target.TakeDamage(amount);
    }
    private void GainBlock(int amount, Damageable target)
    {
        target.GainBlock(amount);
    }
}
