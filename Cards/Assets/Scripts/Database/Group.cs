using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group
{
    public int Id { get; }
    public string Name { get; }
    public string Note { get; }

    public Group(int id, string name, string note)
    {
        Id = id;
        Name = name;
        Note = note;
    }

}
