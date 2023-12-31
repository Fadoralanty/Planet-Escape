using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum CardType{Attack,Skill,Item}
public enum TargetType{Self,SingleEnemy,AllEnemies,RandomEnemy}

public enum ActionType
{
    DealDamage,
    GainBlock,
    DrawCards,
    Heal,
    ApplyBuff,
    Drain,
    GainEnergy,
    Consume
    
}
[CreateAssetMenu(fileName = "New Card")]
public class Card_SO : ScriptableObject
{
    public string CardName;
    [TextArea(2,5)]
    public string CardDescription;
    public int CardCost;
    public CardType CardType;
    public Sprite CardIcon;
    public List<CardAction> _cardActions;
    public TargetType targetType;
    [System.Serializable]
    public struct CardAction
    {
        public ActionType actionType;
        public int amount;
        public int Repetitions;
        public Buff Buff;
    }
}
