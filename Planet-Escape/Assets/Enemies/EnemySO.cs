using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy")]

public class EnemySO : ScriptableObject 
{
    public string EnemyName => enemyName;
    [SerializeField] private string enemyName;
    public int MaxHealth => maxHealth;
    [SerializeField] private int maxHealth;

    public int moneyDrop;
    public List<EnemyActions> EnemyActionsList;
    [System.Serializable]
    public struct EnemyActions
    {
        public ActionType actionType;
        public int amount;
        public int Repetitions;
    }
}
