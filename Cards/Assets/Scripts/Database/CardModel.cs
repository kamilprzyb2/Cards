using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel
{
    public int Id = 0;
    public int GroupId;
    public string Name;
    public int Frequency;
    public Decision[] Decisions;
    public int Type;
    public string Note;

    public CardModel()
    {
        Decisions = new Decision[2];
        Decisions[0] = new Decision();
        Decisions[1] = new Decision();
    }
}
