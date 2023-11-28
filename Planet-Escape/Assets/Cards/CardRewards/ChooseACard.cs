using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseACard : MonoBehaviour
{
    [SerializeField] private List<Card_SO> possibleCardRewards = new List<Card_SO>();
    [SerializeField] private List<CardReward> cardsToChoose = new List<CardReward>();

    public void Skip()
    {
        GameManager.Singleton.GoBackToMapScreen();
    }

    private void Start()
    {
        ShowCards();
    }

    public void ShowCards()
    {
        possibleCardRewards.Shuffle();

        for (int i = 0; i < cardsToChoose.Count; i++)
        {
            cardsToChoose[i].SetData(possibleCardRewards[i]);
        }
    }
    
}
