using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShipInfo : NetworkBehaviour
{
    public int shipID;
    public int shipHP;
    public int ownerID;
    bool canBeDamadge;

    public override void OnNetworkSpawn()
    {
        ShootMissleEvent.current.OnDmgDealt += OnOnDmgDealt;
        ShootMissleEvent.current.OnRoundEnd += OnRoundEnd;
        ownerID = (int)OwnerClientId;
        
        if ((int)OwnerClientId == 1)
        {
            canBeDamadge = true;
        }
        
    }
    void OnRoundEnd(int senderID)
    {
        if (senderID == shipID)
        {
            canBeDamadge = false;
        }
        else
        {
            canBeDamadge = true;
        }
    }
    void OnOnDmgDealt(int shipID)
    {
        if (this.shipID == shipID && canBeDamadge)
        {
            shipHP -= 1;
        }
        if (shipHP == 0)
        {
            GetComponentInParent<UnitsDataBase>().ShipDestroy(this.gameObject);
        }
    }
}
