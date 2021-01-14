using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEditor : MonoBehaviour
{
    public MainEditor editor;
    public Dropdown cardList;
    public Dropdown groupList;
    public Dropdown typeList;
    public InputField note;
    public InputField cardName;
    public DecisionEditor decision1;
    public DecisionEditor decision2;
    public InputField frequency;
    public Button deleteBtn;

    private int selected;

    void Start()
    {
        
    }

    public void UpdateDropdowns()
    {
        UpdateCardList();

        groupList.options.Clear();
        foreach (Group group in editor.groups)
        {
            string text = "#" + group.Id + " " + group.Name;
            groupList.options.Add(new Dropdown.OptionData(text));
        }
        groupList.value = 0;
        groupList.RefreshShownValue();

        decision1.UpdateDropdowns();
        decision2.UpdateDropdowns();
        Clear();
    }

    public void UpdateCardList()
    {
        cardList.options.Clear();
        cardList.options.Add(new Dropdown.OptionData("New Card"));

        foreach (Card card in editor.cards)
        {
            string text = "#" + card.Id + " " + card.Name;
            cardList.options.Add(new Dropdown.OptionData(text));
        }
        cardList.value = 0;
        cardList.RefreshShownValue();
    }
    public void ChangeSelection()
    {
        selected = cardList.value;

        // new card
        if (selected == 0)
        {
            Clear();
        }
        else
        {
            Card selectedCard = editor.cards[selected - 1];
            groupList.value = GetGroupIndex(selectedCard.GroupId);
            // select type
            note.text = selectedCard.Note;
            cardName.text = selectedCard.Name;
            frequency.text = selectedCard.Frequency.ToString();
            typeList.value = selectedCard.Type;
            decision1.Set(selectedCard.Decisions[0]);
            decision2.Set(selectedCard.Decisions[1]);

            deleteBtn.enabled = true;
        }
    }
    public int GetGroupIndex(int groupId)
    {
        int i = 0;
        foreach(Group group in editor.groups)
        {
            if (group.Id == groupId)
            {
                return i;
            }
            i++;
        }
        return 0;
    }
    private void Clear()
    {
        note.text = null;
        cardName.text = null;
        decision1.Clear();
        decision2.Clear();
        frequency.text = "1";
        groupList.value = 0;
        groupList.RefreshShownValue();
        typeList.value = 0;
        typeList.RefreshShownValue();

        deleteBtn.enabled = false;
    }
    public void Save()
    {
        CardModel model = new CardModel();
        model.Name = cardName.text;
        model.Note = note.text;
        model.Frequency = int.Parse(frequency.text);
        model.Type = typeList.value;
        model.Decisions[0] = decision1.Get();
        model.Decisions[1] = decision2.Get();
        model.GroupId = editor.groups[groupList.value].Id;

        if (selected != 0)
        {
            model.Id = editor.cards[cardList.value -1].Id;
        }

        editor.SaveCard(selected, model);
    }
    public void Delete()
    {
        if (selected == 0)
            return;
        editor.DeleteCard(selected);
    }

    public void FilterByGroup()
    {
        int i = 1;
        foreach (Card card in editor.cards)
        {
            if (card.GroupId != editor.groups[groupList.value].Id)
            {
                cardList.options[i].text = "";
            }
            i++;
        }
        cardList.RefreshShownValue();
    }

    public void UnFilter()
    {
        UpdateCardList();
    }

}
