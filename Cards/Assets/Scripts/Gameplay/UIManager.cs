using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image Icon1;
    public Image Icon2;
    public Text Description1;
    public Text Description2;
    public Button Button1;
    public Button Button2;

    private Dictionary<int, Icon> icons;

    public void Start()
    {
        IconDb iconDb = new IconDb();
        icons = new Dictionary<int, Icon>();
        List<Icon> temp = iconDb.GetAll();
        foreach (Icon icon in temp)
        {
            icons.Add(icon.Id, icon);
        }
    }

    public void UpdateCard(Card card)
    {
        Icon1.sprite = Resources.Load<Sprite>("Sprites/CardIcons/" + icons[card.Decisions[0].IconId].Path);
        Icon2.sprite = Resources.Load<Sprite>("Sprites/CardIcons/" + icons[card.Decisions[1].IconId].Path);
        Description1.text = card.Decisions[0].Description;
        Description2.text = card.Decisions[1].Description;
        Button1.GetComponentInChildren<Text>().text = card.Decisions[0].Name;
        Button2.GetComponentInChildren<Text>().text = card.Decisions[1].Name;
    }

}
