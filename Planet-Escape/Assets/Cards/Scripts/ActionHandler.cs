    using System;
    using System.Collections;
using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    public void DoActionMultiple(Card_SO.CardAction action, List<Enemy> targets, Character owner, Card_SO cardSo)
    {
        for (var i = targets.Count - 1; i >= 0; i--)
        {
            DoActionSingle(action, targets[i], owner, cardSo);
        }
    }
    public void DoActionSingle(Card_SO.CardAction action, Character target, Character owner, Card_SO cardSo)
    {
        switch (action.actionType)
        {
            case ActionType.DealDamage:
                int TotalDamage = action.amount;
                if (target.ActiveBuffs.ContainsKey(BuffType.Spikes))
                {
                    DealDamage(target.ActiveBuffs[BuffType.Spikes].buffStacks, owner.Damageable);
                }
                if (owner.ActiveBuffs.ContainsKey(BuffType.AtkUp))
                {
                    TotalDamage += owner.ActiveBuffs[BuffType.AtkUp].currentStacks;
                }
                DealDamage(TotalDamage, target.Damageable);
                break;
            case ActionType.GainBlock:
                GainBlock(action.amount, target.Damageable);
                break;
            case ActionType.DrawCards:
                BattleManager.Singleton.DrawCards(action.amount);
                break;
            case ActionType.Heal:
                target.Damageable.GetHealing(action.amount);
                break;
            case ActionType.ApplyBuff:
                target.AddBuff(action.Buff);
                break;
            case ActionType.Drain:
                DealDamage(action.amount, target.Damageable);
                owner.Damageable.GetHealing(action.amount);
                break;
            case ActionType.GainEnergy:
                BattleManager.Singleton.GainEnergy(action.amount);
                break;
            case ActionType.Consume:
                GameManager.Singleton.RemoveCardFromPlayersDeck(cardSo);
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
