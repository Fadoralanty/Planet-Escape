using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour,IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Vector2 _lastPosition;
    private Quaternion _lastRotation;
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>() ;
        _lastPosition = _rectTransform.anchoredPosition;
        _lastRotation = _rectTransform.localRotation;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(eventData.position.x, eventData.position.y);
        transform.rotation = quaternion.Euler(Vector3.zero);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _lastPosition;
        _rectTransform.rotation = _lastRotation;
        
    }
}
