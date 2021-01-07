using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconEditor : MonoBehaviour
{
    public MainEditor editor;
    public Dropdown iconList;
    public InputField iconName;
    public InputField iconPath;
    public InputField note;
    public Image image;
    public Button deleteBtn;

    private int selected;

    void Start()
    {

    }

    public void UpdateDropdown()
    {
        iconList.options.Clear();
        iconList.options.Add(new Dropdown.OptionData("New Icon"));
        foreach (Icon icon in editor.icons)
        {
            string text = "#" + icon.Id + " " + icon.Name;
            iconList.options.Add(new Dropdown.OptionData(text));
        }
        iconList.value = 0;
        iconList.RefreshShownValue();
        Clear();
    }

    public void ChangeSelection()
    {
        selected = iconList.value;

        // new icon
        if (selected == 0)
        {
            Clear();
        }
        else
        {
            // existing icon selected
            Icon selectedIcon = editor.icons[selected - 1];
            iconName.text = selectedIcon.Name;
            iconPath.text = selectedIcon.Path;
            note.text = selectedIcon.Note;
            UpdateImage();
            deleteBtn.enabled = true;
        }
    }

    public void UpdateImage()
    {
        image.sprite = Resources.Load<Sprite>("Sprites/CardIcons/" + iconPath.text);
    }

    private void Clear()
    {
        // clear all fields
        iconName.text = null;
        iconPath.text = null;
        note.text = null;
        image.sprite = null;
        deleteBtn.enabled = false;
    }

    public void Save()
    {
        editor.SaveIcon(selected, iconName.text, iconPath.text, note.text);
    }
    public void Delete()
    {
        if (selected == 0)
            return;
        editor.DeleteIcon(selected);
    }

}
