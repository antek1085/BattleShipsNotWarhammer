using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MissleShotted : NetworkBehaviour
{
    AudioSource audioSource;
   
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Placable Tile")
        {
            other.GetComponent<Tile>().TileHit();
            Destroy(gameObject);
        }
        else if (other.tag == "Ship")
        {
            Debug.Log("ship");
            DMGSendServerRPC(other.GetComponent<ShipInfo>().shipID);
        }
    }

    [ServerRpc(RequireOwnership = false)]

    void DMGSendServerRPC(int shipID)
    {
        DMGSendClientRPC(shipID);
    }

    [ClientRpc(RequireOwnership = false)]

    void DMGSendClientRPC(int shipId)
    {
        ShootMissleEvent.current.DmgDealt(shipId);
    }
}
