using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipInfo : NetworkBehaviour
{
    public int shipID;
    public int shipHP;
    public int ownerID;
    bool canBeDamadge;
    bool destroyed;

    void Awake()
    {
        ShootMissleEvent.current.OnDmgDealt += OnOnDmgDealt;
        ShootMissleEvent.current.OnRoundEnd += OnRoundEnd;
        ownerID = (int)OwnerClientId;
        if ((int)OwnerClientId == 1)
        {
            canBeDamadge = true;
        }
    }

    public override void OnNetworkSpawn()
    {
        ownerID = (int)OwnerClientId;
        if ((int)OwnerClientId == 1)
        {
            canBeDamadge = true;
        }
    }
    void Start()
    {
        ShootMissleEvent.current.OnDmgDealt += OnOnDmgDealt;
        ShootMissleEvent.current.OnRoundEnd += OnRoundEnd;
        shipID = Random.Range(0, 99999);
    }
    void OnRoundEnd(int senderID)
    {
        Debug.Log(senderID);
        if (senderID != ownerID)
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
            if (shipHP <= 0)
            {
                if (destroyed == false)
                {
                    destroyed = true;
                    GetComponentInParent<UnitsDataBase>().ShipDestroy(this.gameObject);
                }
            }
        }
        
    }
}
