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
    [Header("Player")] 
    public Player Player;
    [Header("Cards")] 
    public Hand Hand;
    public List<Card_SO> drawPile = new List<Card_SO>();
    public List<Card_SO> discardPile = new List<Card_SO>();
    public List<Card_SO> cardsInHand = new List<Card_SO>();
    public int maxCardsInHand = 10;
    public int CardsDrawnPerTurn = 5;
    public ActionHandler ActionHandler;
    public TextMeshProUGUI drawPileText;
    public TextMeshProUGUI discardPileText;
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
    public List<Damageable> CurrentEnemies;
    public Damageable SelectedEnemy;
    
    private Random random = new Random();
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
        
        DrawCards(CardsDrawnPerTurn);
        
        drawPileText.text = drawPile.Count.ToString();
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
        for (var i = discardPile.Count - 1; i > 0; i--)
        {
            var temp = discardPile[i];
            var index = random.Next(0, i + 1);
            discardPile[i] = discardPile[index];
            discardPile[index] = temp;
        }

        drawPile = discardPile;
        discardPile = new List<Card_SO>();
        discardPileText.text = discardPile.Count.ToString();
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
            drawPileText.text = drawPile.Count.ToString();
            cardsDrawn++;
        }
        Hand.UpdateHand();
    }

    public void DiscardHand()
    {
        Hand.DeactivateAllCards();
        foreach (var card in cardsInHand)
        {
            discardPile.Add(card);
        }
        cardsInHand.Clear();
        discardPileText.text = discardPile.Count.ToString();
    }

    public void DiscardCard(Card_SO card)
    {
        discardPile.Add(card);
        cardsInHand.Remove(card);
        //Update DiscardPile Textmeshpro
    }
    public void PlayCard(CardUI card)
    {
        if (card.CardSo.CardCost > CurrentEnergy) return;
        
        //PERFORM CARD ACTION
        PerformCardActions(card.CardSo._cardActions, card.CardSo.targetType);

        CurrentEnergy -= card.CardSo.CardCost;
        EnergyText.text = CurrentEnergy.ToString();

        cardsInHand.Remove(card.CardSo);
        Hand.DeActivateCard(card);
        Hand.UpdateHand();
        
        discardPile.Add(card.CardSo);
        //update discard pile counter TMpro
        discardPileText.text = discardPile.Count.ToString();

    }

    public void PerformCardActions(List<Card_SO.CardAction> cardActions, TargetType targetType)
    {
        foreach (var cardAction in cardActions)
        {
            switch (targetType)
            {
                case TargetType.Self:
                    ActionHandler.DoActionSingle(cardAction, Player.Damageable);
                    break;
                case TargetType.SingleEnemy:
                    ActionHandler.DoActionSingle(cardAction, SelectedEnemy);
                    break;
                case TargetType.AllEnemies:
                    ActionHandler.DoActionMultiple(cardAction, CurrentEnemies);
                    break;
                case TargetType.RandomEnemy:
                    int rndIndex = random.Next(CurrentEnemies.Count);
                    ActionHandler.DoActionSingle(cardAction, CurrentEnemies[rndIndex]);
                    break;
            }
        }
    }
    private void EndCombat()
    {
        //instantiate enemies
    }
    
    private void ChangeTurn()// so termino el turno del player
    {
        if (currentTurn == Turn.Player)
        {
            currentTurn = Turn.Enemy;
            EndturnButton.enabled = false;
            
            DiscardHand();
            
            TurnBannerAnimator.Play("EnemyTurn");
            // REset enemy block
            
            //Show That its the enemy turn
            
            //make all enemies do their action
            
            
        }
        else if(currentTurn == Turn.Enemy)
        {
            //display enemy intent
            currentTurn = Turn.Player;
            TurnBannerAnimator.Play("PlayerTurn");
            EndturnButton.enabled = true;
            //reset player block
            CurrentEnergy = MaxEnergy;
            //show energy
            DrawCards(CardsDrawnPerTurn);
        }
        
    }
    
    private void EndTurn()
    {
        ChangeTurn();
    }
    
}
