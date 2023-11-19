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
}
