using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    [SerializeField] private EnemySO _enemySo;
    [Header("UI")]
    [SerializeField] private Image enemySprite;
    [SerializeField] private Image intentImage;
    [SerializeField] private TextMeshProUGUI intentNum;
    private int _actionIndex;
    public Damageable Damageable => _damageable;
    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _damageable.SetData(_enemySo.MaxHealth);
        _actionIndex = 0;
    }

    public void TakeTurn()
    {
        //hide intent
        //do action
        switch (_enemySo.EnemyActionsList[_actionIndex].actionType)
        {
            case ActionType.DealDamage:
                StartCoroutine(AttackPlayer());
                break;
            case ActionType.GainBlock:
                GainBlock(_enemySo.EnemyActionsList[_actionIndex].amount);
                break;
        }
    }

    IEnumerator AttackPlayer()
    {
        //play attack animation
        
        yield return new WaitForSeconds(0.5f);
        //deal damage
        
        yield return new WaitForSeconds(0.5f);
        _actionIndex += 1;
        if (_actionIndex>_enemySo.EnemyActionsList.Count) { _actionIndex = 0; }
        
    }
    
    private void GainBlock(int block)
    {
        _damageable.GainBlock(block);
        //display block
        _actionIndex += 1;
        if (_actionIndex>_enemySo.EnemyActionsList.Count) { _actionIndex = 0; }
    }
}
