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
    [Header("BLock bar")] 
    [SerializeField] private GameObject blockHealthBar;
    [SerializeField] private GameObject blockIcon;
    [SerializeField] private TextMeshProUGUI blockTMP;
    
    [Header("Sub HealhBar")]
    [SerializeField] private Image _healthBarDelay;
    [SerializeField] private float _loseHealthSpeed= 0.2f;
    [SerializeField] private float _loseHPSpeedNormal= 0.2f;
    [SerializeField] private float _loseHealthSpeedOneShot= 2f;

    [Header("Life Components")]
    [SerializeField] private Damageable _damageable;


    private void Awake()
    {
        _damageable.OnTakeDamage += FillHealthbar;
        _damageable.OnBlockChange += ShowBlueHealthBar;
    }

    private void ShowBlueHealthBar(float block)
    {
        if (block <= 0)
        {
            blockHealthBar.SetActive(false);
            blockIcon.SetActive(false);
            blockTMP.text = "0";

        }
        else
        {
            blockHealthBar.SetActive(true);
            blockIcon.SetActive(true);
            blockTMP.text = block.ToString();
        }
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
