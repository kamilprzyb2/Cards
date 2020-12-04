using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEditor : MonoBehaviour
{
    public GroupEditor groupEditor;
    public IconEditor iconEditor;

    private GroupDb groupDb;
    private IconDb iconDb;
    public List<Group> groups;
    public List<Icon> icons;

    void Start()
    {
        SqliteHelper helper = new SqliteHelper();
        helper.CreateTables();

        groupDb = new GroupDb();
        reloadGroups();
        groupEditor.UpdateDropdown();

        iconDb = new IconDb();
        reloadIcons();
        iconEditor.UpdateDropdown();
    }

    public void SaveGroup(int selected, string name, string note)
    {
        if (selected == 0)
            groupDb.Add(name, note);
        else
            groupDb.Update(groups[selected-1].Id, name, note);

        reloadGroups();
        groupEditor.UpdateDropdown();
    }

    public void DeleteGroup(int selected)
    {
        groupDb.DeleteById(groups[selected-1].Id);

        reloadGroups();
        groupEditor.UpdateDropdown();
    }

    private void reloadGroups()
    {
        groups = groupDb.GetAll();
    }

    public void SaveIcon(int selected, string name, string path, string note)
    {
        if (selected == 0)
            iconDb.Add(name, path, note);
        else
            iconDb.Update(icons[selected - 1].Id, name, path, note);

        reloadIcons();
        iconEditor.UpdateDropdown();
    }

    public void DeleteIcon(int selected)
    {
        iconDb.DeleteById(icons[selected - 1].Id);

        reloadIcons();
        iconEditor.UpdateDropdown();
    }
    private void reloadIcons()
    {
        icons = iconDb.GetAll();
    }
}
