using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyDropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Enemy enemy;
    
    public void OnDrop(PointerEventData eventData)
    {
        CardUI card = eventData.pointerDrag.GetComponent<CardUI>();
        //Debug.Log(card.CardSo.CardName + " was dropped on " + gameObject.name);
        if (card.CardSo.targetType == TargetType.SingleEnemy)
        {
            BattleManager.Singleton.SelectedEnemy = enemy;
            BattleManager.Singleton.PlayCard(card);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
