using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public UIManager uiManager;

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


    private CardDb cardDb;
    private List<Card> cardPool;
    private Card currentCard;

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
        Decision decision = choice == 0 ? currentCard.Decisions[0]: currentCard.Decisions[1];

        food += decision.Values[0];
        population += decision.Values[1];
        faith += decision.Values[2];
        tools += decision.Values[3];

        uiManager.UpdateAttributesInfo(food, population, faith, tools);

        int groupId = decision.GroupId;
        cardPool.Remove(currentCard);
        AddCard(groupId);
        NextCard();

    }
  
    private void NextCard()
    {
        int i = Random.Range(0, cardPool.Count);
        currentCard = cardPool[i];
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


}
