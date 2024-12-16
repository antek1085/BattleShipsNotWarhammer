using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShootingBoardControler : NetworkBehaviour
{
    private List<GameObject> childList = new List<GameObject>();
    
   void Awake()
   {
       for (int i = 0; i < transform.childCount; i++)
       {
           childList.Add(transform.GetChild(i).gameObject);
       }
       
       ShootMissleEvent.current.onMissleHit += CurrentOnonMissleHit;
   }
   void CurrentOnonMissleHit(bool hitInfo, int index, int senderID)
   {
       switch (senderID)
       {
           case 0:
               switch ((int)OwnerClientId)
               {
                   case 0:
                       childList[index].GetComponent<TargetableTile>().CheckShoot(hitInfo);
                       break;
                   case 1:
                       break;
               }
               break;
           case 1:
               switch ((int)OwnerClientId)
               {
                   case 0:
                       break;
                   case 1:
                       childList[index].GetComponent<TargetableTile>().CheckShoot(hitInfo);
                       break;
               }
               break;
       }
   }
}
