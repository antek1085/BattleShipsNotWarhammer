using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UnitsDataBase : NetworkBehaviour
{
    List<GameObject> unitsGameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            unitsGameObjects.Add(transform.GetChild(i).gameObject);
        }
    }
    void Update()
    {
        if (unitsGameObjects.Count == 0)
        {
           EndGameServerRpc((int)OwnerClientId);
        }
    }

    public void ShipDestroy(GameObject unit)
    {
        int index;
        index = unitsGameObjects.IndexOf(unit);
        unitsGameObjects.RemoveAt(index);
    }

    [ServerRpc(RequireOwnership = false)]

    void EndGameServerRpc(int senderID)
    {
        EndGameClientRPC(senderID);
    }

    [ClientRpc]
    void EndGameClientRPC(int senderID)
    {
       GameEndEvent.current.GameEnd(senderID);
    }
    
    
}
