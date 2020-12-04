using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon
{
    public int Id { get; }
    public string Name { get; }
    public string Path { get; }
    public string Note { get; }

    public Icon(int id, string name, string path, string note)
    {
        Id = id;
        Name = name;
        Path = path;
        Note = note;
    }
}
