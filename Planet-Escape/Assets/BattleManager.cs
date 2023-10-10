using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Singleton;
    
    [Header("Energy")]
    public int MaxEnergy;
    public int CurrentEnergy;
    public enum Turn {Player,Enemy};
    [Header("Turns")]
    public Turn currentTurn;
    public Button EndturnButton;
    [Header("Enemies")] 
    public List<GameObject> PossibleEnemies;
    public List<GameObject> CurrentEnemies;
    

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        EndturnButton.onClick.AddListener(EndTurn);
    }

    private void BeginCombat()
    {
        //instantiate enemies
        
        //shuffle deck
        //draw 5 cards
        
        CurrentEnergy = MaxEnergy;
        //update energy UI
    }
    private void InstantiateEnemies()
    {
        GameObject newEnemy = Instantiate(PossibleEnemies[Random.Range(0,PossibleEnemies.Count)]);
    }
    private void EndCombat()
    {
        //instantiate enemies
    }

    private void ChangeTurn()
    {
        if (currentTurn == Turn.Player)
        {
            currentTurn = Turn.Enemy;
            EndturnButton.enabled = false;
            //TODO Discard Hando
            // REset enemy block
        }
        else if(currentTurn == Turn.Enemy)
        {
            //display enemy intent
            currentTurn = Turn.Player;
            EndturnButton.enabled = true;
            //reset player block
            CurrentEnergy = MaxEnergy;
            //show energy
            //draw 5 cards
            
        }
        
    }
    private void EndTurn()
    {
        
    }
    
}
