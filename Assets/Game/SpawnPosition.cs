using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnPosition : NetworkBehaviour
{
   public override void OnNetworkSpawn()
   {
      switch (OwnerClientId)
      {
         case 0:
            transform.position = new Vector3(0, 0, 0);
            break;
         case 1:
            transform.position = new Vector3(1000, 0, 2000);
            break;
      }
   }
}
