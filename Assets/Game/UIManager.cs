using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UIManager : NetworkBehaviour
{
    [SerializeField] GameObject yourTurn, enemyTurn;
    [SerializeField] GameObject YouLose, YouWin;
    [SerializeField] GameObject readyText;

    public override void OnNetworkSpawn()
   {
       ShootMissleEvent.current.OnRoundEnd += RoundEnd;
       GameEndEvent.current.onGameEnd += OnGameEnd;
   }


    void Start()
    {
        if(!IsOwner) return;
        readyText.SetActive(true);
    }
    void OnGameEnd(int senderID)
   {
       if (senderID == (int)OwnerClientId)
       {
           YouLose.SetActive(true);
       }
       else
       {
           YouWin.SetActive(true);
       }
   }
   void RoundEnd(int senderID)
   {
       if(!IsOwner) return;
       if (senderID == (int)OwnerClientId)
       {
           enemyTurn.SetActive(true);
           StartCoroutine(DisableText(enemyTurn));
       }
       else
       {
           yourTurn.SetActive(true);
           StartCoroutine(DisableText(yourTurn));
       }
   }

   void Update()
   {
       if(!IsOwner) return;
       Debug.Log("F1");
       if (Input.GetKeyUp(KeyCode.F1))
       {
           Ready();
           readyText.SetActive(false);
       }
   }

   IEnumerator DisableText(GameObject text)
   {
       yield return new WaitForSeconds(1);
       text.SetActive(false);
   }

   public void ExitGame()
   {            
       Application.Quit();
   }

   public void Ready()
   {
       ReadyServerRpc((int)OwnerClientId);
   }

   [ServerRpc(RequireOwnership = true)]

   void ReadyServerRpc(int senderID)
   {
       ReadyClientRpc(senderID);
   }

   [ClientRpc]

   void ReadyClientRpc(int senderID)
   {
       GameEndEvent.current.ReadyClick(senderID);
   }
   
   
}
