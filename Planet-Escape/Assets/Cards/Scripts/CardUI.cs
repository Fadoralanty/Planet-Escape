using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Card_SO CardSo;
    public RectTransform RectTransform;
    public Image Image;
    public Image border;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI cost;
    public Dragable Dragable;
    public ZoomIn ZoomIn;

    private void Start()
    {
        Dragable.OnEndDragAction += OnEndDragHandler;
    }

    private void OnEndDragHandler()
    {
        if (CanBePlayed() && CardSo.targetType != TargetType.SingleEnemy && !Dragable.isInHandZone)
        {
            BattleManager.Singleton.PlayCard(this);
        }
    }

    public void SetData(Card_SO cardSo)
    {
        CardSo = cardSo;
        Image.sprite = cardSo.CardIcon;
        Name.text = CardSo.CardName;
        Description.text = CardSo.CardDescription;
        cost.text = CardSo.CardCost.ToString();
        gameObject.name = CardSo.CardName;
    }

    public bool CanBePlayed()
    {
        return CardSo.CardCost <= BattleManager.Singleton.CurrentEnergy;
    }

    private void OnDestroy()
    {
        Dragable.OnEndDragAction -= OnEndDragHandler;
    }
}
