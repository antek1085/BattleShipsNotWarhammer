using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : NetworkBehaviour
{
   [SerializeField] Material newMaterial;
   Material actualMaterial;
   [SerializeField] GameObject missle;
   public bool isRaycasted; 
   int index;
   [SerializeField] private LayerMask layerMask2hp,layerMask4hp;
   GameObject spawnedObjectTransform;
   bool didHit;
   [SerializeField] Material materialHit, materialMiss;
   
   

   void Awake()
   {
      didHit = false;
      index = transform.GetSiblingIndex();
      isRaycasted = false;
      actualMaterial = GetComponent<MeshRenderer>().material;
   }

   void Update()
   {
      if (isRaycasted == true && actualMaterial != newMaterial)
      {
         actualMaterial = newMaterial;
         GetComponent<MeshRenderer>().material = actualMaterial;
      }
      else if (isRaycasted == false && actualMaterial != null)
      {
         actualMaterial = null;
         GetComponent<MeshRenderer>().material = actualMaterial;
      }
      isRaycasted = false;
   }

   public void OnMissleShoot()
   {
      SpawnServerRpc();
   }
   [ServerRpc(RequireOwnership = false)]
   private void SpawnServerRpc()
   {
      spawnedObjectTransform = Instantiate(missle, new Vector3(this.transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.Euler(0, 0, 0));
      spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
   }

   public void TileHit()
   {
      
      RaycastHit hit;
      if (Physics.Raycast(this.transform.position, transform.transform.up, out hit, Mathf.Infinity, layerMask2hp))
      {
         didHit = true;
         gameObject.GetComponent<MeshRenderer>().material = materialHit;
      }
      else if (Physics.Raycast(this.transform.position, transform.transform.up, out hit, Mathf.Infinity, layerMask4hp))
      {
         didHit = true;
         gameObject.GetComponent<MeshRenderer>().material = materialHit;
      }
      else
      {
         didHit = false;
         gameObject.GetComponent<MeshRenderer>().material = materialMiss;
      }
      int ownerId; 
      switch ((int)OwnerClientId)
      {
         case 0:
            ownerId = 1;
            GetComponentInParent<BoardController>().HitInfo(didHit,index,ownerId);
            break;
         case 1:
             ownerId = 0;
             GetComponentInParent<BoardController>().HitInfo(didHit,index,ownerId);
            break;
      }
   }

}
