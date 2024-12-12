using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMissleEvent : MonoBehaviour
{
   public static ShootMissleEvent current;

   void Awake()
   {
      current = this;
   }

   public event Action<int> onMissleShoot;
   public void MissleShoot(int index)
   {
      if (onMissleShoot != null)
      {
         onMissleShoot(index);
      }
   }
}
