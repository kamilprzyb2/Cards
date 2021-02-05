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
    public bool hoverRightBtn = false;
    public bool hoverLeftBtn = false;

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
        uiManager.UnHighlightAttributes();

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
        uiManager.HighlightCounters(decision.Values[0], decision.Values[1], decision.Values[2], decision.Values[3]);

        cardsToGo--;
        if (CheckEndCondition())
            return;

        uiManager.ShowFakeCards(currentCard.Decisions[0].Description, currentCard.Decisions[0].IconId,
            currentCard.Decisions[1].Description, currentCard.Decisions[1].IconId);

        int groupId = decision.GroupId;
        cardPool.Remove(currentCard);
        AddCard(groupId);
        NextCard();
    }

    public void ButtonHover(int btn)
    {
        if (locked)
            return;

        Decision decision;
        // 0 = left button, else right
        decision = btn == 0 ? currentCard.Decisions[0] : currentCard.Decisions[1];

        if (btn == 0)
            hoverLeftBtn = true;
        else
            hoverRightBtn = true;

        uiManager.HighlightAttributes(decision.Values[0] != 0, decision.Values[1] != 0, decision.Values[2] != 0, decision.Values[3] != 0);
    }

    public void ButtonHoverExit()
    {
        hoverLeftBtn = false;
        hoverRightBtn = false;

        uiManager.UnHighlightAttributes();
    }

    public void UnlockGame()
    {
        locked = false;

        if (hoverRightBtn)
        {
            Decision decision = currentCard.Decisions[1];
            uiManager.HighlightAttributes(decision.Values[0] != 0, decision.Values[1] != 0, decision.Values[2] != 0, decision.Values[3] != 0);
        }
        else if (hoverLeftBtn)
        {
            Decision decision = currentCard.Decisions[0];
            uiManager.HighlightAttributes(decision.Values[0] != 0, decision.Values[1] != 0, decision.Values[2] != 0, decision.Values[3] != 0);
        }

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
