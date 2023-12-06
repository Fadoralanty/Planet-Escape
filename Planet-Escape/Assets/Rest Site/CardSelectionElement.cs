using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelectionElement : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnClick;
    public Card_SO CardSo;
    public Image Image;
    public Image coloredFrame;
    public Image border;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI cardType;
    public Color AttackColor;
    public Color SkillColor;
    public Color ItemColor;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetData(Card_SO cardSo)
    {
        CardSo = cardSo;
        Image.sprite = cardSo.CardIcon;
        Name.text = CardSo.CardName;
        Description.text = CardSo.CardDescription;
        cost.text = CardSo.CardCost.ToString();
        gameObject.name = CardSo.CardName;
        switch (cardSo.CardType)
        {
            case CardType.Attack:
                coloredFrame.color = AttackColor;
                break;
            case CardType.Skill:
                coloredFrame.color = SkillColor;
                break;
            case CardType.Item:
                coloredFrame.color = ItemColor;
                break;
        }

        cardType.text = cardSo.CardType.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Singleton.RemoveCardFromPlayersDeck(CardSo);
        OnClick.Invoke();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.Play("OnHoverExit");
        border.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.Play("OnHoverEnter");
        border.gameObject.SetActive(true);

    }
}
