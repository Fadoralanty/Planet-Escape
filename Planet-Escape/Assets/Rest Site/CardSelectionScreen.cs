using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionScreen : MonoBehaviour
{
    public List<CardSelectionElement> Elements = new List<CardSelectionElement>();
    private void Awake()
    {
        for (var i = 0; i < GameManager.Singleton.PlayersDeck.Count; i++)
        {
            Elements[i].gameObject.SetActive(true);
            Elements[i].SetData(GameManager.Singleton.PlayersDeck[i]);
        }
    }
}
