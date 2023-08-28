using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [SerializeField] private int cardSpacingMultiplier = 15;
    [SerializeField] private float cardRotationMultiplier = 15;
    [SerializeField] private float cardHeightMultiplier = 15;
    [SerializeField] private List<GameObject> cardsInHand;
    
    private void Awake()
    {
        SetCardsSpacing();
        SetCardsHeight();
        RotateCards();
    }

    private void RotateCards()
    {
        foreach (var card in cardsInHand)
        {
            float handRatio = (float)card.transform.GetSiblingIndex() / (cardsInHand.Count - 1);
            float rotation = Mathf.SmoothStep(-cardRotationMultiplier, cardRotationMultiplier, handRatio);
            card.transform.rotation = Quaternion.Euler(0,0, -rotation); 
        }
    }

    private void SetCardsHeight()
    {
        foreach (var card in cardsInHand)
        {
            float handRatio = (float)card.transform.GetSiblingIndex() / (cardsInHand.Count - 1);
            float height = Mathf.Abs( Mathf.Lerp(cardHeightMultiplier, -cardHeightMultiplier, handRatio));
            card.transform.position -= new Vector3(0, height, 0);
            // if (handRatio is 0 or 1 )
            // {
            //     card.transform.position -= new Vector3(0, height, 0);
            // }
        }
    }
    private void SetCardsSpacing()
    {
        if (cardsInHand.Count < 1) { return; }
        foreach (var card in cardsInHand)
        {
            float handRatio = (float)card.transform.GetSiblingIndex() / (cardsInHand.Count - 1);
            float spacing = Mathf.Lerp(-cardSpacingMultiplier / 2, cardSpacingMultiplier / 2, handRatio);
            card.transform.position += new Vector3(spacing , 0, 0); 
        }
    }
}
