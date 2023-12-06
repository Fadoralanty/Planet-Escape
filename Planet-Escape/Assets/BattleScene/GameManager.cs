using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    public List<Card_SO> PlayersDeck=new List<Card_SO>();
    public PlayerSO PlayerSo;
    public int playerHealth;
    public int playerMaxHealth;
    public NodeType CurrentNodeType = NodeType.MinorEnemy;
    public bool IsNewGame;
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        playerMaxHealth = PlayerSo.MaxHealth;
        playerHealth = playerMaxHealth;
    }
    
    public void NewGame()
    {
        IsNewGame = true;
        SceneManager.LoadScene("Map");
        playerMaxHealth = PlayerSo.MaxHealth;
        playerHealth = playerMaxHealth;
    }

    public void RemoveCardFromPlayersDeck(Card_SO card)
    {
        if (PlayersDeck.Contains(card))
        {
            PlayersDeck.Remove(card);
        }
    }
    public void GoBackToMapScreen()
    {
        SceneManager.LoadScene("Map");
    }
    
    public void HealPlayer(int amount)
    {
        playerHealth += amount;
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }    
    public void HealPlayerPercentage(float percentage)
    {
        if (percentage>100){ percentage = 100; }
        else if (percentage<0) { percentage = 0; }

        float amountToHeal = playerMaxHealth * (percentage / 100);
        
        playerHealth += Mathf.CeilToInt(amountToHeal);
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }
    
    
}
