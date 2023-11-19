using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private float moveBackToHandTime = 2f;
    [SerializeField] private CanvasGroup _canvasGroup;
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
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        //_rectTransform.anchoredPosition = _lastPosition;
        _rectTransform.rotation = _lastRotation;
        StartCoroutine(MoveBackToHand(moveBackToHandTime));
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

    }
    public void SetLastTransform(Vector2 newPos, Quaternion newRot)
    {
        _lastPosition = newPos;
        _lastRotation= newRot;
    }
    IEnumerator MoveBackToHand(float duration)
    {
        float timeElapsed = 0;
        Vector2 startPosition = _rectTransform.anchoredPosition;
        while (timeElapsed < duration)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(startPosition,_lastPosition, timeElapsed/duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        _rectTransform.anchoredPosition = _lastPosition;
    }
}
