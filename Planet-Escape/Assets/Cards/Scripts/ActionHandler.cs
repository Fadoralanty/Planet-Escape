    using System;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    public void DoActionMultiple(Card_SO.CardAction action, List<Damageable> targets)
    {
        foreach (var target in targets)
        {
            DoActionSingle(action, target);
        }
    }
    public void DoActionSingle(Card_SO.CardAction action, Damageable target)
    {
        switch (action.actionType)
        {
            case ActionType.DealDamage:
                DealDamage(action.amount, target);
                break;
            case ActionType.GainBlock:
                GainBlock(action.amount, target);
                break;
            case ActionType.DrawCards:
                BattleManager.Singleton.DrawCards(action.amount);
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
