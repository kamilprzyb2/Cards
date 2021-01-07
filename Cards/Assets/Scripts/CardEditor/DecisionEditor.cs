using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionEditor : MonoBehaviour
{
    public CardEditor cardEditor;
    public InputField decisionName;
    public InputField description;    
    public InputField value1;
    public InputField value2;
    public InputField value3;
    public InputField value4;
    public Dropdown groupList;
    public Dropdown iconList;
    public Image icon;

    public void Clear()
    {
        // clear fields
        decisionName.text = null;
        description.text = null;
        value1.text = null;
        value2.text = null;
        value3.text = null;
        value4.text = null;
        // change groups
        groupList.value = 0;
        iconList.value = 0;
        UpdateImage();
    }

    public void UpdateDropdowns()
    {
        iconList.options.Clear();
        foreach (Icon icon in cardEditor.editor.icons)
        {
            string text = "#" + icon.Id + " " + icon.Name;
            iconList.options.Add(new Dropdown.OptionData(text));
        }
        iconList.value = 0;
        iconList.RefreshShownValue();


        groupList.options.Clear();
        foreach (Group group in cardEditor.editor.groups)
        {
            string text = "#" + group.Id + " " + group.Name;
            groupList.options.Add(new Dropdown.OptionData(text));
        }
        groupList.value = 0;
        groupList.RefreshShownValue();

        UpdateImage();
    }

    public void Set(Decision decision)
    {
        decisionName.text = decision.Name;
        description.text = decision.Description;
        value1.text = decision.Values[0].ToString();
        value2.text = decision.Values[1].ToString();
        value3.text = decision.Values[2].ToString();
        value4.text = decision.Values[3].ToString();
        iconList.value = GetIconIndex(decision.IconId);
        iconList.RefreshShownValue();
        UpdateImage();
        groupList.value = cardEditor.GetGroupIndex(decision.GroupId);
        groupList.RefreshShownValue();
    }

    public Decision Get()
    {
        Decision decision = new Decision();
        decision.Name = decisionName.text;
        decision.Description = description.text;
        decision.Values[0] = int.Parse(value1.text);
        decision.Values[1] = int.Parse(value2.text);
        decision.Values[2] = int.Parse(value3.text);
        decision.Values[3] = int.Parse(value4.text);
        decision.GroupId = cardEditor.editor.groups[groupList.value].Id;
        decision.IconId = cardEditor.editor.icons[iconList.value].Id;

        return decision;
    }

    public void UpdateImage()
    {
        if (cardEditor.editor.icons.Count == 0)
            return;
        icon.sprite = Resources.Load<Sprite>("Sprites/CardIcons/" + cardEditor.editor.icons[iconList.value].Path);
    }

    private int GetIconIndex(int iconId)
    {
        int i = 0;
        foreach (Icon icon in cardEditor.editor.icons)
        {
            if (icon.Id == iconId)
            {
                return i;
            }
            i++;
        }
        return 0;
    }
}
