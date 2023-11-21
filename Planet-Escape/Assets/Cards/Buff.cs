using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Buff
{
    public BuffType BuffType;
    public int StatModifier;
    public int TurnAmountDuration;
    public int buffStacks;
}
public enum BuffType
{
    Regeneration,
    
}