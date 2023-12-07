using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomIn : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Transform handTransform;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float onMouseHoverHeight = 150f;
    private Animator _animator;
    private Quaternion _lastRotation;
    private Vector2 _lastPosition;
    private int lastSiblingIndex;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        handTransform = transform.parent;
    }

    private void Start()
    {
        Transform transform1 = transform;
        lastSiblingIndex = (transform1).GetSiblingIndex();
        _lastRotation = transform1.rotation;
        _lastPosition = transform1.position;
    }

    public void SetLastTransform(Vector2 newPos, Quaternion newRot)
    {
        _lastPosition = newPos;
        _lastRotation= newRot;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.Play("OnHoverEnter");
        transform.SetSiblingIndex(handTransform.childCount);
        rectTransform.rotation = Quaternion.Euler(Vector3.zero);
        _lastPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, onMouseHoverHeight);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.Play("OnHoverExit");
        transform.SetSiblingIndex(lastSiblingIndex);
        rectTransform.rotation = _lastRotation;
        rectTransform.anchoredPosition = _lastPosition;
    }

}
