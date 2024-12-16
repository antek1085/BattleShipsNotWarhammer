using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameEndEvent : MonoBehaviour
{
   public static GameEndEvent current;
   void Awake()
   {
      current = this;
   }

   public event Action<int> OnReadyClick;

   public void ReadyClick(int senderID)
   {
      if (OnReadyClick != null)
      {
         OnReadyClick(senderID);
      }
   }

   public event Action onGameStart;

   public void GameStart()
   {
      if (onGameStart != null)
      {
         onGameStart();
      }
   }


   public event Action<int> onGameEnd;

   public void GameEnd(int senderID)
   {
      if (onGameEnd != null)
      {
         onGameEnd(senderID);
      }
   }
}
