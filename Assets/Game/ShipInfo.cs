using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ShipInfo : NetworkBehaviour
{
    public int shipID;
    public int shipHP;
    public int ownerID;
    bool canBeDamadge;
    bool destroyed;

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
            if (destroyed == false)
            {
                destroyed = true;
                GetComponentInParent<UnitsDataBase>().ShipDestroy(this.gameObject);
            }
        }
    }
}
