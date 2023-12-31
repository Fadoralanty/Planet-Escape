using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffCollection : MonoBehaviour
{
    public List<BuffUIItem> inactiveBuffUI = new List<BuffUIItem>();
    public Dictionary<BuffType, BuffUIItem> ActiveBuffUIItems = new Dictionary<BuffType, BuffUIItem>();
    public Character character;
    public BuffsIconsSO BuffsIcons;
    private void Awake()
    {
        character.OnBuffAdded += OnBuffAdded;
        character.OnBuffRemoved += OnBuffRemoved;
        character.OnBuffUpdated += OnBuffUpdated;
    }

    private void OnBuffUpdated(Buff buff)
    {
        if (!ActiveBuffUIItems.ContainsKey(buff.BuffType)) return;
        ActiveBuffUIItems[buff.BuffType].TMP.text = buff.currentStacks.ToString();

    }

    private void OnBuffRemoved(Buff buff)
    {
        if (!ActiveBuffUIItems.ContainsKey(buff.BuffType)) return;
        
        inactiveBuffUI.Add(ActiveBuffUIItems[buff.BuffType]);
        ActiveBuffUIItems[buff.BuffType].gameObject.SetActive(false);
        ActiveBuffUIItems.Remove(buff.BuffType);
    }

    private void OnBuffAdded(Buff buff)
    {
        if (ActiveBuffUIItems.ContainsKey(buff.BuffType))
        {
            ActiveBuffUIItems[buff.BuffType].TMP.text = buff.currentStacks.ToString();
        }
        else
        {
            inactiveBuffUI[0].gameObject.SetActive(true);
            ActiveBuffUIItems.Add(buff.BuffType,inactiveBuffUI[0]);
            inactiveBuffUI.RemoveAt(0);
            ActiveBuffUIItems[buff.BuffType].TMP.text = buff.currentStacks.ToString();
            AddBuffIcon(buff);
        }
    }

    private void AddBuffIcon(Buff buff)
    {
        ActiveBuffUIItems[buff.BuffType].Image.sprite = buff.BuffType switch
        {
            BuffType.Regeneration => BuffsIcons.RegenIcon,
            BuffType.Poison => BuffsIcons.PoisonIcon,
            BuffType.Burn => BuffsIcons.BurnIcon,
            BuffType.Ice => BuffsIcons.IceIcon,
            BuffType.Slow => BuffsIcons.SlowIcon,
            BuffType.Fast => BuffsIcons.FastIcon,
            BuffType.Stun => BuffsIcons.StunIcon,
            BuffType.Spikes => BuffsIcons.SpikesIcon,
            BuffType.AtkUp => BuffsIcons.AtkUp,
            _ => ActiveBuffUIItems[buff.BuffType].Image.sprite
        };
    }
    private void OnDestroy()
    {
        character.OnBuffAdded -= OnBuffAdded;
        character.OnBuffRemoved -= OnBuffRemoved;
    }
}
