using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Main HealthBar")]
    [SerializeField] private Image _healthBar;
    [SerializeField] private Color _healthBarColor = new Color(1, 0, 0, 1);

    [Header("Sub HealhBar")]
    [SerializeField] private Image _healthBarDelay;
    [SerializeField] private Color _healthBarDelayColor = new Color(1, 0.8f, 0, 0.8f);
    [SerializeField] private float _loseHealthSpeed= 0.2f;
    [SerializeField] private float _loseHPSpeedNormal= 0.2f;
    [SerializeField] private float _loseHealthSpeedOneShot= 2f;
    [Header("Backgorund HealthBar")]
    [SerializeField] private Image _healthBarBackground;
    [SerializeField] private Color _healthBarBackgroundColor = new Color(0.4f, 0, 0, 1);

    [Header("Life Components")]
    [SerializeField] private Damageable _damageable;


    private void Awake()
    {
        _damageable.OnTakeDamage += FillHealthbar;
        InitHealthBarColor();
    }
    
    private void InitHealthBarColor()
    {
        _healthBar.color = _healthBarColor;
        _healthBarDelay.color = _healthBarDelayColor;
        _healthBarBackground.color = _healthBarBackgroundColor;
    }
    private void Update()
    {
        Fill2ndHealthbar();
    }
    public void FillHealthbar(float currentlife)
    {
        _healthBar.fillAmount = currentlife/_damageable.MaxLife;
        _loseHealthSpeed = _loseHPSpeedNormal;
    }   
    public void Fill2ndHealthbar()
    {
        if (_healthBar.fillAmount == 0)
        {
            _loseHealthSpeed = _loseHealthSpeedOneShot;
        }
        if (_healthBarDelay.fillAmount>_healthBar.fillAmount)
        {
            _healthBarDelay.fillAmount -= _loseHealthSpeed  * Time.deltaTime;
        }
        else
        {
            _healthBarDelay.fillAmount = _healthBar.fillAmount;
        }
    }

}
