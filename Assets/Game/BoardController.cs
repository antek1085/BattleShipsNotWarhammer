using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BoardController : NetworkBehaviour
{
    private List<GameObject> childList = new List<GameObject>();
    [SerializeField] ShootingBoardControler _shootingBoardControler;
    [SerializeField]List<AudioClip> hitSounds = new List<AudioClip>();
    AudioSource audioSource;
    [FormerlySerializedAs("audioSources")]
    [SerializeField]List<AudioClip> missSound = new List<AudioClip>();

    void Awake()                        
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childList.Add(transform.GetChild(i).gameObject);
        }
        audioSource = GetComponent<AudioSource>();
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
        switch (hitInfo)
        {
            case true: 
                int randomIndexHit = Random.Range(0,hitSounds.Count -1);
                audioSource.PlayOneShot(hitSounds[randomIndexHit]);
                break;
            case false:
                int randomIndexMiss = Random.Range(0,missSound.Count -1);
                audioSource.PlayOneShot(missSound[randomIndexMiss]);
                break;
        }
    }

    void TestOneMore(bool hitInfo,int index,int senderId)
    {
        ShootMissleEvent.current.MissleHit(hitInfo,index,senderId);
    }
    
    
    
    //Send Info to Tile on Shoot to shoot Missle

    void SendInformationToTile(int index,int clientID)
    {
        childList[index].GetComponent<Tile>().OnMissleShoot(clientID);
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
                TestClientRpc(index,clientID,new ClientRpcParams{ Send = {TargetClientIds = new List<ulong>{1} }} );
                break;
            case 1:
                TestClientRpc(index,clientID,new ClientRpcParams{ Send = {TargetClientIds = new List<ulong>{0} }} );
                break;
            default:
                break;
        }
    }
    
    [ClientRpc]
    void TestClientRpc(int index,int clientId,ClientRpcParams clientRpcSendParams)
     {
         SendInformationToTile(index,clientId);
     }
}
