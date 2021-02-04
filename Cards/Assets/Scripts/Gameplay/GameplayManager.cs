using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public UIManager uiManager;
    public EndScreen EndWindow;

    [Header("Attributes")]
    [Tooltip("Change to modify start values")]
    [SerializeField]
    private int food = 20;
    [SerializeField]
    private int population = 1;
    [SerializeField]
    private int faith = 30;
    [SerializeField]
    private int tools = 10;

    [Header("Win Condition")]
    [SerializeField]
    private int cardsToGo = 50;

    private CardDb cardDb;
    private List<Card> cardPool;
    private Card currentCard;

    private bool locked = false;


    void Start()
    {
        uiManager.UpdateAttributesInfo(food, population, faith, tools);

        cardDb = new CardDb();
        cardPool = cardDb.GetStarterCards();

        NextCard();       
    }

    /// <summary>
    /// make one of 2 choices
    /// </summary>
    /// <param name="decision">0 = left, else right</param>
    public void MakeDecision(int choice)
    {
        if (locked)
            return;

        locked = true;

        if (currentCard.Type == 404)
        {
            // if it's 404 card, restart
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        Decision decision = choice == 0 ? currentCard.Decisions[0]: currentCard.Decisions[1];

        food += decision.Values[0];
        population += decision.Values[1];
        faith += decision.Values[2];
        tools += decision.Values[3];

        uiManager.UpdateAttributesInfo(food, population, faith, tools);

        cardsToGo--;
        if (CheckEndCondition())
            return;

        int groupId = decision.GroupId;
        cardPool.Remove(currentCard);
        AddCard(groupId);
        NextCard();

        locked = false;
    }
  
    private void NextCard()
    {
        if (cardPool.Count == 0)
        {
            currentCard = Card.card404();
        }
        else
        {
            int i = Random.Range(0, cardPool.Count);
            currentCard = cardPool[i];
        }
        uiManager.UpdateCard(currentCard);
    }

    private void AddCard(int groupId)
    {
        List<Card> cards = cardDb.GetByGroup(groupId);

        int totalPool = 0;
        foreach (Card card in cards)
            totalPool += card.Frequency;

        int i = Random.Range(0, totalPool);
        foreach (Card card in cards)
        {
            if (i < card.Frequency)
            {
                cardPool.Add(card);
                return;
            }
            i -= card.Frequency;
        }
    }

    private bool CheckEndCondition()
    {
        if (food <= 0)
        {
            Debug.Log("Lost by food");
            EndWindow.EndingStart();
            return true;
        }
        if (population <= 0)
        {
            Debug.Log("Lost by population");
            EndWindow.EndingStart();
            return true;
        }
        if (faith <= 0)
        {
            Debug.Log("Lost by faith");
            EndWindow.EndingStart();
            return true;
        }
        if (tools <= 0)
        {
            Debug.Log("Lost by tools");
            EndWindow.EndingStart();
            return true;
        }

        if (cardsToGo <= 0)
        {
            Debug.Log("Victory!");
            EndWindow.EndingStart();
            return true;
        }
        return false;
    }

}
