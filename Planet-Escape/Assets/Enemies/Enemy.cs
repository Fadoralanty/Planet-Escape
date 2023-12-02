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
        _damageable.OnDie += OnDieHandler;
    }

    private void OnDieHandler()
    {
        BattleManager.Singleton.CurrentEnemies.Remove(this);
        gameObject.SetActive(false);
    }

    public void TakeTurn()
    {
        //hide intent
        //do action
        EnemySO.EnemyActions action = _enemySo.EnemyActionsList[_actionIndex];
        switch (action.actionType)
        {
            case ActionType.DealDamage:
                StartCoroutine(AttackPlayer(action));
                break;
            case ActionType.GainBlock:
                GainBlock(action.amount);
                break;
            case ActionType.ApplyBuff:
                ApplyBuff(action.Buff);
                break;
        }
    }

    private void ApplyBuff(Buff buff)
    {
        switch (buff.BuffType)
        {
            case BuffType.Regeneration:
                break;
            case BuffType.Poison:
                Target.AddBuff(buff);
                break;
        }
        EndTurn();
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
        EnemySO.EnemyActions action = _enemySo.EnemyActionsList[_actionIndex];
        intentNum.text = action.amount.ToString();
        switch (action.actionType)
        {
            case ActionType.DealDamage:
                intentImage.sprite = intentIconsSo.AttackIcon;
                break;
            case ActionType.GainBlock:
                intentImage.sprite = intentIconsSo.BlockIcon;
                break;
            case ActionType.Heal:
                break;
            case ActionType.ApplyBuff:
                switch (action.Buff.BuffType)
                {
                    case BuffType.Poison:
                        intentImage.sprite = intentIconsSo.PoisonIcon;
                        break;
                }
                break;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _damageable.OnDie -= OnDieHandler;
    }
}
