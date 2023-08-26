using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomIn : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private Transform handTransform;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.Play("OnHoverEnter");
        transform.SetSiblingIndex(handTransform.childCount);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = new Vector3(transform.position.x, onMouseHoverHeight);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.Play("OnHoverExit");
        transform.SetSiblingIndex(lastSiblingIndex);
        transform.rotation = _lastRotation;
        transform.position = _lastPosition;
    }

}
