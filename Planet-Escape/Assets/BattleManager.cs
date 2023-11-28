using System;
using System.Collections;
using System.Collections.Generic;
using Map;
using TMPro;
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

    [Header("Victory & Defeat")] 
    public GameObject GameOverScreen;

    [Header("Turns")]
    public Turn currentTurn;
    public Button EndturnButton;
    public Animator TurnBannerAnimator;
    [Header("Enemies")] 
    public List<Encounter> PossibleMinorEnemies;
    public List<Encounter> PossibleEnemies;
    public List<Encounter> PossibleEliteEnemies;
    public List<Enemy> CurrentEnemies;
    public Enemy SelectedEnemy;
    
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
        Player.Damageable.OnDie += Defeat;
        GameOverScreen.SetActive(false);
        BeginCombat();
    }

    private void BeginCombat()
    {
        InstantiateEnemies();
        
        foreach (var enemy in CurrentEnemies)
        {
            enemy.Target = Player;
        }
        ShowEnemiesIntents();
        
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
    private void InstantiateEnemies()
    {
        switch (GameManager.Singleton.CurrentNodeType)
        {
            case NodeType.MinorEnemy:
                int rnd = random.Next(PossibleMinorEnemies.Count);
                Encounter encounter = Instantiate(PossibleMinorEnemies[rnd], transform.parent).GetComponent<Encounter>();
                encounter.transform.SetSiblingIndex(1);
                foreach (var enemy in encounter.Enemies)
                {
                    CurrentEnemies.Add(enemy);
                }
                break;
            case NodeType.EliteEnemy:
                break;
            case NodeType.Boss:
                break;
            case NodeType.Mystery:
                break;
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
        discardPileText.text = discardPile.Count.ToString();

    }

    public void PerformCardActions(List<Card_SO.CardAction> cardActions, TargetType targetType)
    {
        foreach (var cardAction in cardActions)
        {
            switch (targetType)
            {
                case TargetType.Self:
                    ActionHandler.DoActionSingle(cardAction, Player);
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
    private void Victory()
    {
        
    }    
    private void Defeat()
    {
        GameOverScreen.SetActive(true);
    }
    
    private void ChangeTurn()
    {
        if (currentTurn == Turn.Player)// si termino el turno del player
        {
            currentTurn = Turn.Enemy;
            EndturnButton.enabled = false;
            
            DiscardHand();
            
            
            foreach (var enemy in CurrentEnemies)
            {
                enemy.Damageable.RemoveBlock();
            }
            
            //Show That its the enemy turn
            TurnBannerAnimator.Play("EnemyTurn");
            
            //make all enemies do their action
            StartCoroutine(ExecuteEnemyTurn());
        }
        else if(currentTurn == Turn.Enemy)// si termino el turno de los enemigos
        {
            //display enemy intent
            ShowEnemiesIntents();
            
            currentTurn = Turn.Player;
            TurnBannerAnimator.Play("PlayerTurn");
            EndturnButton.enabled = true;
            
            //reset player block
            Player.Damageable.RemoveBlock();
            
            CurrentEnergy = MaxEnergy;
            EnergyText.text = CurrentEnergy.ToString();
            
            DrawCards(CardsDrawnPerTurn);
            Hand.UpdateHand();
        }
        
    }

    IEnumerator ExecuteEnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        foreach (var enemy in CurrentEnemies)
        {
            enemy.isMidTurn = true;
            enemy.TakeTurn();
            while (enemy.isMidTurn)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        ChangeTurn();
    }

    private void ShowEnemiesIntents()
    {
        foreach (var enemy in CurrentEnemies)
        {
            enemy.ShowIntent();
        }
    }
    private void EndTurn()
    {
        ChangeTurn();
    }

    private void OnDestroy()
    {
        Player.Damageable.OnDie -= Defeat;
    }
}
