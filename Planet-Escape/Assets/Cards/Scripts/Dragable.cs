using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public bool isInHandZone;
    public Action OnEndDragAction;
    public CardUI CardUI;
    public GameObject playerCrosshair;
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
        playerCrosshair.SetActive(false);
    }

    private void Update()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (CardUI.CardSo.targetType)
        {
            case TargetType.Self:
                playerCrosshair.SetActive(true);
                break;
            case TargetType.SingleEnemy:
                break;
            case TargetType.AllEnemies:
                break;
            case TargetType.RandomEnemy:
                break;
        }
        transform.position = new Vector3(eventData.position.x, eventData.position.y);
        transform.rotation = quaternion.Euler(Vector3.zero);
        _canvasGroup.alpha = 0.5f;
        _canvasGroup.blocksRaycasts = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        BattleManager.Singleton.SelectedCard = null;
        playerCrosshair.SetActive(false);
        _rectTransform.rotation = _lastRotation;
        StartCoroutine(MoveBackToHand(moveBackToHandTime));
        ResetCanvasGroup();
        if (!isInHandZone)
        {
            _rectTransform.position = _lastPosition;
            OnEndDragAction?.Invoke();
        }

        isInHandZone = false;
        
    }

    public void ResetCanvasGroup()
    {
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        BattleManager.Singleton.SelectedCard = CardUI.CardSo;
    }
}
