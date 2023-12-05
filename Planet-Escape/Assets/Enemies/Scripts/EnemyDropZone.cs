using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyDropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private GameObject crossHair;
    public void OnDrop(PointerEventData eventData)
    {
        CardUI card = eventData.pointerDrag.GetComponent<CardUI>();
        //Debug.Log(card.CardSo.CardName + " was dropped on " + gameObject.name);
        if (card==null) {return; }
        if (card.CardSo.targetType == TargetType.SingleEnemy)
        {
            BattleManager.Singleton.SelectedEnemy = enemy;
            BattleManager.Singleton.PlayCard(card);
            BattleManager.Singleton.SelectedCard = null;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Card_SO SelectedCard = BattleManager.Singleton.SelectedCard;
        if (SelectedCard == null) { return; }

        if (SelectedCard.targetType == TargetType.SingleEnemy)
        {
            crossHair.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        crossHair.SetActive(false);
    }
}
