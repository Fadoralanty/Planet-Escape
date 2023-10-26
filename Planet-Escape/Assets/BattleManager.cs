using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Singleton;

    [Header("Cards")] 
    public Hand Hand;
    public List<Card_SO> drawPile = new List<Card_SO>();
    public List<Card_SO> discardPile = new List<Card_SO>();
    public List<Card_SO> cardsInHand = new List<Card_SO>();
    public int maxCardsInHand = 10;
    [Header("Energy")]
    public int MaxEnergy;
    public int CurrentEnergy;
    public TextMeshProUGUI EnergyText;
    public enum Turn {Player,Enemy};
    [Header("Turns")]
    public Turn currentTurn;
    public Button EndturnButton;
    public Animator TurnBannerAnimator;
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
        BeginCombat();
    }

    private void BeginCombat()
    {
        //int enemiesToSpawn = 1;
        //InstantiateEnemies(enemiesToSpawn);
        
        discardPile.Clear();
        drawPile.Clear();
        discardPile.AddRange(GameManager.Singleton.PlayersDeck);
        ShuffleDeck();
        DrawCards(5);
        
        CurrentEnergy = MaxEnergy;
        //update energy UI
        TurnBannerAnimator.Play("PlayerTurn");
    }
    private void InstantiateEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newEnemy = Instantiate(PossibleEnemies[UnityEngine.Random.Range(0, PossibleEnemies.Count)]);
        }
    }

    private void ShuffleDeck()
    {
        Random random = new Random();
        for (var i = discardPile.Count - 1; i > 0; i--)
        {
            var temp = discardPile[i];
            var index = random.Next(0, i + 1);
            discardPile[i] = discardPile[index];
            discardPile[index] = temp;
        }

        drawPile = discardPile;
        discardPile = new List<Card_SO>();
        //update discard pile counter/text
    }
    public void DrawCards(int amountToDraw)
    {
        int cardsDrawn = 0;
        while(cardsDrawn < amountToDraw && cardsInHand.Count <= maxCardsInHand)
        {
            if(drawPile.Count < 1)
                ShuffleDeck();

            cardsInHand.Add(drawPile[0]);
            Hand.ActivateCard(drawPile[0]);
            drawPile.Remove(drawPile[0]);
            // Update drawpile numbre counter TMpro
            cardsDrawn++;
        }
        Hand.UpdateHand();
    }

    public void DiscardHand()
    {
        foreach (var card in cardsInHand)
        {
            DiscardCard(card);
        }
        Hand.DeactivateAllCards();
    }

    public void DiscardCard(Card_SO card)
    {
        discardPile.Add(card);
        //Update DiscardPile Textmeshpro
    }
    public void PlayCard(CardUI card)
    {
        if (card.CardSo.CardCost > CurrentEnergy) return;
        
        //PERFORM CARD ACTION

        CurrentEnergy -= card.CardSo.CardCost;
        EnergyText.text = CurrentEnergy.ToString();

        cardsInHand.Remove(card.CardSo);
        Hand.DeActivateCard(card);
        
        discardPile.Add(card.CardSo);
        //update discard pile counter TMpro


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
            DiscardHand();
            
            // REset enemy block
            
            //Show That its the enemy turn
            
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
        ChangeTurn();
    }
    
}
