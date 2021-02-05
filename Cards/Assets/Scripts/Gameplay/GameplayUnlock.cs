using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUnlock : MonoBehaviour
{
    // this script exists solely because it has to be attached to an object to be used via animation event
    public GameplayManager manager;

    public void activate()
    {
        manager.UnlockGame();
    }
}
