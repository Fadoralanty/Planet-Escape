using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Text")] [SerializeField] private TextMeshProUGUI TMP;
    [Header("Main HealthBar")]
    [SerializeField] private Image _healthBar;

    private void Start()
    {
        float currentLife = GameManager.Singleton.playerHealth;
        float MaxLife = GameManager.Singleton.playerMaxHealth;
        _healthBar.fillAmount = currentLife/MaxLife;
        TMP.text = currentLife + " / " + MaxLife;
    }
}
