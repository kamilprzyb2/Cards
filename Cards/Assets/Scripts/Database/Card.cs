using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int Id { get; }
    public int GroupId { get; }
    public string Name { get; }
    public int Frequency { get; }
    public Decision[] Decisions { get; }
    public int Type { get; }
    public string Note { get; }

    public Card(CardModel model)
    {
        Id = model.Id;
        GroupId = model.GroupId;
        Name = model.Name;
        Frequency = model.Frequency;
        Decisions = model.Decisions;
        Type = model.Type;
        Note = model.Note;
    }

    /// <summary>
    /// returns special 404 card, to use when you don't have any cards left
    /// </summary>
    /// <returns></returns>
    public static Card card404()
    {
        CardModel model = new CardModel();
        model.Id = -1;
        model.GroupId = -1;
        model.Name = "Card 404";
        model.Note = "In theory player should never see this";
        model.Type = 404;
        model.Decisions = new Decision[2];
        model.Decisions[0] = new Decision();
        model.Decisions[1] = new Decision();
        model.Decisions[0].Description = "I'll be honest, I have no idea how you got there. This part of the story isn't even finished yet!";
        model.Decisions[1].Description = "Can we pretend this never happend and start over?";
        model.Decisions[0].Name = "Restart";
        model.Decisions[1].Name = "Restart";
        model.Decisions[0].GroupId = -1;
        model.Decisions[1].GroupId = -1;
        model.Decisions[0].Values = new int[4];
        model.Decisions[1].Values = new int[4];

        model.Decisions[0].IconId = 1;
        model.Decisions[1].IconId = 1;

        for (int i = 0; i < 2; i++)
        {
            for (int j =0; j < 4; j++)
            {
                model.Decisions[i].Values[j] = 0;
            }
        }

        return new Card(model);
    }
}