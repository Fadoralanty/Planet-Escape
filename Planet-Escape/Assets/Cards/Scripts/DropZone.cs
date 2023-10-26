using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardUI card= eventData.pointerDrag.GetComponent<CardUI>();
        // Debug.Log(card.CardSo.CardName + " was dropped on " + gameObject.name);
        if (card.CardSo.CardTargetType == CardTargetType.Enemy)
        {
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
