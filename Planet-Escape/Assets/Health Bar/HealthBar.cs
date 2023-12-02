using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Text")] [SerializeField] private TextMeshProUGUI TMP;
    [Header("Main HealthBar")]
    [SerializeField] private Image _healthBar;
    [SerializeField] private Color _healthBarColor = new Color(1, 0, 0, 1);
    [Header("BLock bar")] 
    [SerializeField] private GameObject blockHealthBar;

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
        _damageable.OnBlockChange += ShowBlueHealthBar;
        InitHealthBarColor();
    }

    private void ShowBlueHealthBar(float block)
    {
        if (block <= 0)
        {
            blockHealthBar.SetActive(false);
            TMP.text = _damageable.CurrentLife + " / " + _damageable.MaxLife;
        }
        else
        {
            blockHealthBar.SetActive(true);
            TMP.text = _damageable.CurrentLife + " / " + _damageable.MaxLife + " + " + block;
        }
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
    public void FillHealthbar(float currentlife, float damage) 
    {
        _healthBar.fillAmount = currentlife/_damageable.MaxLife;
        _loseHealthSpeed = _loseHPSpeedNormal;
        TMP.text = _damageable.CurrentLife + " / " + _damageable.MaxLife;
        ShowBlueHealthBar(_damageable.CurrentBlock);
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

    private void OnDestroy()
    {
        _damageable.OnTakeDamage -= FillHealthbar;
        _damageable.OnBlockChange -= ShowBlueHealthBar;
    }
}
