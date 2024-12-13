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
        Debug.Log("Spawn Missle");
        mode = GameMode.AutoHostOrClient;
        Debug.Log(mode);
        ShootMissleEvent.current.MissleShoot(index,mode);
    }
}
