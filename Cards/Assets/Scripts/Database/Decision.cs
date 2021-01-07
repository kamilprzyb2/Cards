using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision
{
    public string Name;
    public string Description;
    public int IconId;
    public int[] Values;
    public int GroupId;
    
    public Decision()
    {
        Values = new int[4];
    }
}
