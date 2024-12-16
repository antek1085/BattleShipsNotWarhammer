using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShootMouseController : NetworkBehaviour
{
    Ray ray;
    Vector3 mousePosition;
    [SerializeField] Camera _camera;
    [SerializeField] LayerMask layerMask;
    bool canHeShoot;
    [SerializeField] GameObject ShootingBoard;
    bool hostReady, clientReady;
    
   

    void Awake()
    {
        /*canHeShoot = false;
        ShootMissleEvent.current.onMissleHit += OnMissleHit;
        ShootMissleEvent.current.OnRoundEnd += OnRoundEnd;*/
    }
    
    public override void OnNetworkSpawn()
    {
        hostReady = false;
        clientReady = false;
        canHeShoot = false;
        ShootMissleEvent.current.onMissleHit += OnMissleHit;
        ShootMissleEvent.current.OnRoundEnd += OnRoundEnd;
        GameEndEvent.current.OnReadyClick += OnReadyClick;
       GameEndEvent.current.onGameStart += OnGameStart;
    }
    void OnGameStart()
    {
        if ((int)OwnerClientId == 0)
        {
            canHeShoot = true;
        }
    }
    void OnReadyClick(int senderID)
    {
        switch (senderID)
        {
            case 0:
                hostReady = true;
                break;
            case 1:
                clientReady = true;
                break;
        }

        if (hostReady && clientReady)
        {
            ReadyServerRPC();
        }
    }
    void OnRoundEnd(int senderID)
    {
        if (senderID != (int)OwnerClientId)
        {
            canHeShoot = true;
        }
    }
    void OnMissleHit(bool hitInfo, int arg2, int senderID)
    {
        if (senderID == (int)OwnerClientId)
        {
            switch (hitInfo)
            {
                case true:
                    canHeShoot = hitInfo;
                    break;
                case false:
                    NewRoundServerRPC(senderID);
                    ShootingBoard.SetActive(!ShootingBoard.activeSelf);
                    break;
            }
        }
    }

    void Update()
    {
        if(!IsOwner) return;

        mousePosition = Input.mousePosition;
        
        if (canHeShoot)
        {
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask))
            {
                hit.transform.GetComponent<TargetableTile>().isRaycasted = true;
            } 
        }
        
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            SpawnRay();
        }
        if (Input.GetKeyUp(KeyCode.M) && canHeShoot)
        {
            ShootingBoard.SetActive(!ShootingBoard.activeSelf);
        }
    }

    void SpawnRay()
    {
        if (canHeShoot)
        {
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.GetComponent<TargetableTile>().isActive == true)
                {
                    canHeShoot = false;
                    hit.transform.GetComponent<TargetableTile>().ShootMissle();   
                }
            } 
        }
    }

    [ServerRpc(RequireOwnership = false)]

    void NewRoundServerRPC(int senderID)
    {
        NewRoundClientRPC(senderID);
    }

    [ClientRpc(RequireOwnership = false)]
    void NewRoundClientRPC(int SenderID)
    {
        ShootMissleEvent.current.RoundEnd(SenderID);
    }


    [ServerRpc(RequireOwnership = false)]

    void ReadyServerRPC()
    {
        ReadyClientRPC();
    }

    [ClientRpc(RequireOwnership = false)]

    void ReadyClientRPC()
    {
        GameEndEvent.current.GameStart();
    }
}
