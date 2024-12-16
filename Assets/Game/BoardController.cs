using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BoardController : NetworkBehaviour
{
    private List<GameObject> childList = new List<GameObject>();
    [SerializeField] ShootingBoardControler _shootingBoardControler;

    void Awake()                        
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childList.Add(transform.GetChild(i).gameObject);
        }
        
        ShootMissleEvent.current.onMissleShoot += OnMissleShoot; 
    }
    
    
    public override void OnNetworkSpawn()
    {
        ShootMissleEvent.current.onMissleShoot += OnMissleShoot; 
    }

    //Get Info from Tile if hit or miss

    public void HitInfo(bool didHit, int index, int senderID)
    {
        HitInfoServerRPC(didHit,index,senderID,new ServerRpcParams());
    }
    
    
    [ServerRpc(RequireOwnership = false)]
    void HitInfoServerRPC(bool hitInfo,int index,int senderId,ServerRpcParams serverRpcReceiveParams)
    {
        HitInforClientRPC( hitInfo,index,senderId);
    }
    [ClientRpc(RequireOwnership = false)]
    void HitInforClientRPC(bool hitInfo,int index,int senderId)
    {
        TestOneMore(hitInfo,index,senderId);
    }

    void TestOneMore(bool hitInfo,int index,int senderId)
    {
        ShootMissleEvent.current.MissleHit(hitInfo,index,senderId);
    }
    
    
    
    //Send Info to Tile on Shoot to shoot Missle

    void SendInformationToTile(int index)
    {
        childList[index].GetComponent<Tile>().OnMissleShoot();
    }

    void OnMissleShoot(int index,int clientID)
    {
        if(IsOwner) return;
        VariableChangeServerRPC(index,clientID);
    }
    
    [ServerRpc(RequireOwnership = false)]
     void VariableChangeServerRPC(int index,int clientID)
    {
        switch (clientID)
        {
            case 0:
                TestClientRpc(index,new ClientRpcParams{ Send = {TargetClientIds = new List<ulong>{1} }} );
                break;
            case 1:
                TestClientRpc(index,new ClientRpcParams{ Send = {TargetClientIds = new List<ulong>{0} }} );
                break;
            default:
                break;
        }
    }
    
    [ClientRpc]
    void TestClientRpc(int index,ClientRpcParams clientRpcSendParams)
     {
         SendInformationToTile(index);
     }
}
