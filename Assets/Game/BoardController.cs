using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private GameMode _gameMode;
    private List<GameObject> childList = new List<GameObject>();
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childList.Add(transform.GetChild(i).gameObject);
        }

        _gameMode = GameMode.AutoHostOrClient;
    }

    void Start()
    {
        ShootMissleEvent.current.onMissleShoot += OnMissleShoot;
    }

    void OnMissleShoot(int index,GameMode mode)
    {
        if (mode != _gameMode)
        { 
            childList[index].GetComponent<Tile>().OnMissleShoot();
        }
    }
}
