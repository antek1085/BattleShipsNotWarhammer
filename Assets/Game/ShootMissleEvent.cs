using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMissleEvent : MonoBehaviour
{
   public static ShootMissleEvent current;
   int check;

   void Awake()
   {
      current = this;
   }
   
   
   public event Action<int, int> onMissleShoot;
   
   public void MissleShoot(int index, int clientID)
   {
      if (onMissleShoot != null)
      {
         onMissleShoot(index, clientID);
      }
   }

   public event Action<bool,int,int> onMissleHit;

   public void MissleHit(bool hitInfo, int index, int senderId)
   {
      if (onMissleHit != null)
      {
         onMissleHit(hitInfo, index, senderId);
      }
   }

   public event Action<int> OnRoundEnd;

   public void RoundEnd(int senderId)
   {
      if (OnRoundEnd != null)
      {
         OnRoundEnd(senderId);
      }
   }

   public event Action<int> OnDmgDealt;

   public void DmgDealt(int shipID)
   {
      if (OnDmgDealt != null)
      {
         OnDmgDealt(shipID);
      }
   }

}
