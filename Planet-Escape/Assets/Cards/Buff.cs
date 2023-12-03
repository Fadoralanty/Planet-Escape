using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Buff
{
    public BuffType BuffType;
    public int buffStacks;
    public int currentStacks;

    public Buff(Buff buff)
    {
        BuffType = buff.BuffType;
        buffStacks = buff.buffStacks;
        currentStacks = buff.currentStacks;
    }
}
public enum BuffType
{
    None,
    Regeneration,
    Poison,
    Burn,
    Ice,
    Slow,
    Fast,
    Stun,
    Spikes
}