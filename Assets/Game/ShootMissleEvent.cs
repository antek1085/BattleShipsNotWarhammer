using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ShootMissleEvent : MonoBehaviour
{
   public static ShootMissleEvent current;
   

   void Awake()
   {
      current = this;
   }

   public event Action<int,GameMode> onMissleShoot;
   public void MissleShoot(int index,GameMode mode)
   {
      if (onMissleShoot != null)
      {
         onMissleShoot(index,mode);
      }
   }
}
