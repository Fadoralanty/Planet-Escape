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

    private void Awake()
    {
        character.OnBuffAdded += OnBuffAdded;
        character.OnBuffRemoved += OnBuffRemoved;
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
        }
    }

    private void OnDestroy()
    {
        character.OnBuffAdded -= OnBuffAdded;
        character.OnBuffRemoved -= OnBuffRemoved;
    }
}
