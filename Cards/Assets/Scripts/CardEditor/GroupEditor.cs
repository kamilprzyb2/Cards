using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupEditor : MonoBehaviour
{
    public MainEditor editor;
    public Dropdown groupList;
    public InputField groupName;
    public InputField note;

    private int selected;

    void Start()
    {

    }

    public void UpdateDropdown()
    {
        groupList.options.Clear();
        groupList.options.Add(new Dropdown.OptionData("New Group"));
        foreach (Group group in editor.groups)
        {
            string text = "#" + group.Id + " " + group.Name;
            groupList.options.Add(new Dropdown.OptionData(text));
        }
        groupList.value = 0;
        Clear();
    }
    
    public void ChangeSelection()
    {
        selected = groupList.value;

        // new group
        if (selected == 0)
        {
            Clear();
        }
        else
        {
            // existing group selected
            Group selectedGroup = editor.groups[selected - 1];
            groupName.text = selectedGroup.Name;
            note.text = selectedGroup.Note;
        }
    }

    private void Clear()
    {
        // clear all fields
        groupName.text = null;
        note.text = null;
    }

    public void Save()
    {
        editor.SaveGroup(selected, groupName.text, note.text);
    }
    public void Delete()
    {
        if (selected == 0)
            return;
        editor.DeleteGroup(selected);
    }

}
