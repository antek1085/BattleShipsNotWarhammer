using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class TargetableTile : NetworkBehaviour
{
    int index;
    GameMode mode;
    void Awake()
    {
        index = transform.GetSiblingIndex();
    }
    public void ShootMissle()
    {
        mode = GameMode.AutoHostOrClient;
        ShootMissleEvent.current.MissleShoot(index,mode);
    }
}
