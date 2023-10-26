using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType{Attack,Skill,Item}
public enum CardTargetType{Self,Enemy,AllEnemies}
[CreateAssetMenu(fileName = "New Card")]
public class Card_SO : ScriptableObject
{
    public string CardName;
    public string CardDescription;
    public int CardCost;
    public int CardEffectAmount;
    public Sprite CardIcon;
    public CardType CardType;
    public CardTargetType CardTargetType;
}
