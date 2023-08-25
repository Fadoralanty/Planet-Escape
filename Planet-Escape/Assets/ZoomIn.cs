using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomIn : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Transform handTransform;
    private Animator _animator;
    private int lastSiblingIndex;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        handTransform = transform.parent;
    }

    private void Start()
    {
        lastSiblingIndex = transform.GetSiblingIndex();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.Play("OnHoverExit");
        transform.SetSiblingIndex(lastSiblingIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.Play("OnHoverEnter");
        transform.SetSiblingIndex(handTransform.childCount);
    }
}
