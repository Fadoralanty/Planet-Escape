using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public Character Target;
    public bool isMidTurn;
    [SerializeField] private EnemySO _enemySo;
    [Header("UI")]
    [SerializeField] private IntentIconsSO intentIconsSo;
    [SerializeField] private Image enemySprite;
    [SerializeField] private Image intentImage;
    [SerializeField] private TextMeshProUGUI intentNum;
    private int _actionIndex;
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
                StartCoroutine(AttackPlayer(_enemySo.EnemyActionsList[_actionIndex]));
                break;
            case ActionType.GainBlock:
                GainBlock(_enemySo.EnemyActionsList[_actionIndex].amount);
                break;
        }
    }

    IEnumerator AttackPlayer(EnemySO.EnemyActions enemyAction)
    {
        //play attack animation
        
        yield return new WaitForSeconds(0.5f);
        //deal damage
        Target.Damageable.TakeDamage(enemyAction.amount);
        yield return new WaitForSeconds(0.5f);
        
        EndTurn();

    }
    
    private void GainBlock(int block)
    {
        _damageable.GainBlock(block);
        //display block
        EndTurn();
    }

    private void EndTurn()
    {
        _actionIndex += 1;
        if (_actionIndex >= _enemySo.EnemyActionsList.Count) { _actionIndex = 0; }

        isMidTurn = false;
    }

    public void ShowIntent()
    {
        intentNum.text = _enemySo.EnemyActionsList[_actionIndex].amount.ToString();
        switch (_enemySo.EnemyActionsList[_actionIndex].actionType)
        {
            case ActionType.DealDamage:
                intentImage.sprite = intentIconsSo.AttackIcon;
                break;
            case ActionType.GainBlock:
                intentImage.sprite = intentIconsSo.BlockIcon;
                break;
        }
    }
}
