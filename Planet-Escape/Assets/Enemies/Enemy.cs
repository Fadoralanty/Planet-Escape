using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : Character, IPointerEnterHandler, IPointerExitHandler
{
    public Character Target;
    public bool isMidTurn;
    [SerializeField] private EnemySO _enemySo;
    [Header("UI")]
    [SerializeField] private IntentIconsSO intentIconsSo;
    [SerializeField] private BuffsIconsSO buffsIconsSo;
    [SerializeField] private Image enemySprite;
    [SerializeField] private Image intentImage;
    [SerializeField] private TextMeshProUGUI intentNum;
    [SerializeField] private TextMeshProUGUI name;
    private int _actionIndex;
    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _damageable.SetData(_enemySo.MaxHealth);
        _actionIndex = 0;
        _damageable.OnDie += OnDieHandler;
        name.text = _enemySo.EnemyName;
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
            case ActionType.Drain:
                StartCoroutine(DrainHp(action));
                break;
        }
    }

    private void ApplyBuff(Buff buff)
    {
        switch (buff.BuffType)
        {
            //Self buffs
            case BuffType.Regeneration:
            case BuffType.Spikes:
            case BuffType.Fast:
                AddBuff(buff);
                break;
            //Debuffs to player
            case BuffType.Poison:
            case BuffType.Burn:
            case BuffType.Ice:
            case BuffType.Slow:
            case BuffType.Stun:
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
    IEnumerator DrainHp(EnemySO.EnemyActions enemyAction)
    {
        //play attack animation
        
        yield return new WaitForSeconds(0.5f);
        //deal damage
        Target.Damageable.TakeDamage(enemyAction.amount);
        Damageable.GetHealing(enemyAction.amount);
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
                intentImage.sprite = action.Buff.BuffType switch
                {
                    BuffType.Poison => buffsIconsSo.PoisonIcon,
                    BuffType.Regeneration => buffsIconsSo.RegenIcon,
                    BuffType.Burn => buffsIconsSo.BurnIcon,
                    BuffType.Ice => buffsIconsSo.IceIcon,
                    BuffType.Slow => buffsIconsSo.SlowIcon,
                    BuffType.Fast => buffsIconsSo.FastIcon,
                    BuffType.Stun => buffsIconsSo.StunIcon,
                    BuffType.Spikes => buffsIconsSo.SpikesIcon,
                    _ => intentImage.sprite
                };
                intentNum.text = action.Buff.buffStacks.ToString();
                break;
            case ActionType.DrawCards:
                break;
            case ActionType.Drain:
                intentImage.sprite = intentIconsSo.DrainIcon;

                break;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _damageable.OnDie -= OnDieHandler;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        name.transform.parent.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        name.transform.parent.gameObject.SetActive(false);
    }
}
