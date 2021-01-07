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

}