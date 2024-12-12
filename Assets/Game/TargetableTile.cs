using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableTile : MonoBehaviour
{
    int index;
    void Awake()
    {
        index = transform.GetSiblingIndex();
    }
    public void ShootMissle()
    {
        ShootMissleEvent.current.MissleShoot(index);
    }
}
