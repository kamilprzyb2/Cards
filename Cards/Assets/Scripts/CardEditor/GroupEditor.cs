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
    public Button deleteBtn;

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
        groupList.RefreshShownValue();
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

            // enable delete button if its not start group
            deleteBtn.enabled = (selected > 1);
        }
    }

    private void Clear()
    {
        groupName.text = null;
        note.text = null;
        deleteBtn.enabled = false;
    }

    public void Save()
    {
        editor.SaveGroup(selected, groupName.text, note.text);
    }
    public void Delete()
    {
        if (selected < 2)
            return;
        editor.DeleteGroup(selected);
    }

}
