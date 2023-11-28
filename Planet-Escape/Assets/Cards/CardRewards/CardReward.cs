using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardReward : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private Card_SO _cardSo;
    [SerializeField] private Image Image;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI SkipButtonTMP;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void SetData(Card_SO cardSo)
    {
        _cardSo = cardSo;
        Image.sprite = _cardSo.CardIcon;
        Name.text = _cardSo.CardName;
        Description.text = _cardSo.CardDescription;
        cost.text = _cardSo.CardCost.ToString();
        gameObject.name = _cardSo.CardName;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.Play("OnHoverExit");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.Play("OnHoverEnter");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Singleton.PlayersDeck.Add(_cardSo);
        SkipButtonTMP.text = "Continue";
        transform.parent.gameObject.SetActive(false);
    }
}
