﻿using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Values")]
    public Text FoodValue;
    public Text PopulationValue;
    public Text FaithValue;
    public Text ToolsValue;
    [Header("Icons")]
    public Image Food;
    public Image Population;
    public Image Faith;
    public Image Tools;
    [Header("Card")]
    public Image Icon1;
    public Image Icon2;
    public Text Description1;
    public Text Description2;
    public Button Button1;
    public Button Button2;
    [Header("Fake Cards")]
    public RectTransform FakeCard1;
    public Text FakeDescription1;
    public Image FakeImage1;
    public RectTransform FakeCard2;
    public Text FakeDescription2;
    public Image FakeImage2;

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

    public void UpdateAttributesInfo(int food, int population, int faith, int tools)
    {
        FoodValue.text = food.ToString();
        PopulationValue.text = population.ToString();
        FaithValue.text = faith.ToString();
        ToolsValue.text = tools.ToString();
    }

    public void HighlightAttributes(bool food, bool population, bool faith, bool tools)
    {
        Food.GetComponent<Animator>().SetBool("highlight", food);
        Population.GetComponent<Animator>().SetBool("highlight", population);
        Faith.GetComponent<Animator>().SetBool("highlight", faith);
        Tools.GetComponent<Animator>().SetBool("highlight", tools);
    }

    public void UnHighlightAttributes()
    {
        HighlightAttributes(false, false, false, false);
    }

    public void ShowFakeCards(string text1, int icon1, string text2, int icon2)
    {
        FakeDescription1.text = text1;
        FakeImage1.sprite = Resources.Load<Sprite>("Sprites/CardIcons/" + icons[icon1].Path);
        FakeCard1.GetComponent<Animator>().SetTrigger("curl");

        FakeDescription2.text = text2;
        FakeImage2.sprite = Resources.Load<Sprite>("Sprites/CardIcons/" + icons[icon2].Path);
        FakeCard2.GetComponent<Animator>().SetTrigger("curl");
    }

    public void HighlightCounters(int food, int population, int faith, int tools)
    {
        if (food > 0)
            FoodValue.GetComponent<Animator>().SetTrigger("green");
        else if (food <0)
            FoodValue.GetComponent<Animator>().SetTrigger("red");

        if (population > 0)
            PopulationValue.GetComponent<Animator>().SetTrigger("green");
        else if (population < 0)
            PopulationValue.GetComponent<Animator>().SetTrigger("red");

        if (faith > 0)
            FaithValue.GetComponent<Animator>().SetTrigger("green");
        else if (faith < 0)
            FaithValue.GetComponent<Animator>().SetTrigger("red");

        if (tools > 0)
            ToolsValue.GetComponent<Animator>().SetTrigger("green");
        else if (tools < 0)
            ToolsValue.GetComponent<Animator>().SetTrigger("red");
    }

}
