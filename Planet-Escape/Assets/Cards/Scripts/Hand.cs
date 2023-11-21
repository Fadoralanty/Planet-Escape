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
    [SerializeField] private List<CardUI> InactiveCards;
    public List<CardUI> ActiveCards => activeCards;
    [SerializeField] private List<CardUI> activeCards;


    private float _proportion;
    private void Awake()
    {
        activeCards = new List<CardUI>();
        foreach (var card in InactiveCards)
        {
            card.gameObject.SetActive(false);
        }
    }

    public void ActivateCard(Card_SO cardSo)
    {
        activeCards.Add(InactiveCards[0]);
        InactiveCards[0].gameObject.SetActive(true);
        InactiveCards[0].SetData(cardSo);
        InactiveCards.Remove(InactiveCards[0]);
    }
    public void DeActivateCard(CardUI cardUI)
    {
        InactiveCards.Add(cardUI);
        activeCards.Remove(cardUI);
        cardUI.transform.SetSiblingIndex(10);
        cardUI.gameObject.SetActive(false);
    }

    public void DeactivateAllCards()
    {
        foreach (var cardUI in activeCards)
        {
            InactiveCards.Add(cardUI);
            cardUI.RectTransform.localPosition = Vector3.zero;
            cardUI.transform.rotation = quaternion.Euler(Vector3.zero);
            cardUI.gameObject.SetActive(false);
            cardUI.transform.SetSiblingIndex(10);
        }
        activeCards.Clear();
    }

    public void ResetCardsTransform()
    {
        foreach (var card in activeCards)
        {
            card.RectTransform.localPosition = Vector3.zero;
            card.transform.rotation = quaternion.Euler(Vector3.zero);
            card.Dragable.ResetCanvasGroup();
        }
    }
    public void UpdateHand()
    {
        ResetCardsTransform();
        
        FanCards();
        
        foreach (var card in activeCards)
        {
            Vector2 newPosition = card.RectTransform.anchoredPosition;
            Quaternion newRotation = card.RectTransform.localRotation;
            
            card.Dragable.SetLastTransform(newPosition, newRotation);
            card.ZoomIn.SetLastTransform(newPosition, newRotation);
            
            card.border.gameObject.SetActive(card.CanBePlayed());
        }
    }

    private void FanCards()
    {
        _proportion = activeCards.Count / 10f;
        SetCardsSpacing();
        SetCardsHeight();
        RotateCards();
    }
    private void RotateCards()
    {
        foreach (var card in activeCards)
        {
            float handRatio = (float)card.transform.GetSiblingIndex() / (activeCards.Count - 1);
            float rotation = Mathf.SmoothStep(-cardRotationMultiplier * _proportion,
                cardRotationMultiplier* _proportion, handRatio);
            card.transform.rotation = Quaternion.Euler(0,0, -rotation); 
        }
    }

    private void SetCardsHeight()
    {
        foreach (var card in activeCards)
        {
            float handRatio = (float)card.transform.GetSiblingIndex() / (activeCards.Count - 1);
            float height = Mathf.Abs( Mathf.Lerp(cardHeightMultiplier * _proportion,
                -cardHeightMultiplier * _proportion, handRatio));
            card.transform.position -= new Vector3(0, height, 0);
            // if (handRatio is 0 or 1 )
            // {
            //     card.transform.position -= new Vector3(0, height, 0);
            // }
        }
    }
    private void SetCardsSpacing()
    {
        if (activeCards.Count < 1) { return; }
        foreach (var card in activeCards)
        {
            float handRatio = (float)card.transform.GetSiblingIndex() / (activeCards.Count - 1);
            float spacing = Mathf.Lerp(-cardSpacingMultiplier * _proportion,
                cardSpacingMultiplier * _proportion , handRatio);
            card.transform.position += new Vector3(spacing , 0, 0); 
        }
    }
}
