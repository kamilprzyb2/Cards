using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEditor : MonoBehaviour
{
    public GroupEditor groupEditor;
    public IconEditor iconEditor;
    public CardEditor cardEditor;

    private GroupDb groupDb;
    private IconDb iconDb;
    private CardDb cardDb;
    public List<Group> groups;
    public List<Icon> icons;
    public List<Card> cards;

    void Start()
    {
        SqliteHelper helper = new SqliteHelper();

        groupDb = new GroupDb();
        ReloadGroups();
        groupEditor.UpdateDropdown();

        iconDb = new IconDb();
        ReloadIcons();
        iconEditor.UpdateDropdown();

        cardDb = new CardDb();
        ReloadCards();
        cardEditor.UpdateDropdowns();
    }

    public void SaveGroup(int selected, string name, string note)
    {
        if (selected == 0)
            groupDb.Add(name, note);
        else
            groupDb.Update(groups[selected-1].Id, name, note);

        ReloadGroups();
        groupEditor.UpdateDropdown();
        cardEditor.UpdateDropdowns();
    }

    public void DeleteGroup(int selected)
    {
        groupDb.DeleteById(groups[selected-1].Id);

        ReloadGroups();
        groupEditor.UpdateDropdown();
        cardEditor.UpdateDropdowns();
    }

    private void ReloadGroups()
    {
        groups = groupDb.GetAll();
    }

    public void SaveIcon(int selected, string name, string path, string note)
    {
        if (selected == 0)
            iconDb.Add(name, path, note);
        else
            iconDb.Update(icons[selected - 1].Id, name, path, note);

        ReloadIcons();
        iconEditor.UpdateDropdown();
        cardEditor.UpdateDropdowns();
    }

    public void DeleteIcon(int selected)
    {
        iconDb.DeleteById(icons[selected - 1].Id);

        ReloadIcons();
        iconEditor.UpdateDropdown();
        cardEditor.UpdateDropdowns();
    }

    private void ReloadIcons()
    {
        icons = iconDb.GetAll();
    }

    public void SaveCard(int selected, CardModel model)
    {
        if (selected == 0)
            cardDb.Add(model);
        else
            cardDb.Update(model);

        ReloadCards();
        cardEditor.UpdateDropdowns();
    }

    public void DeleteCard(int selected)
    {
        cardDb.DeleteById(selected);
        cardEditor.UpdateDropdowns();
    }

    private void ReloadCards()
    {
        cards = cardDb.GetAll();
    }    
}
