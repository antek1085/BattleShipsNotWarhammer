using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlaceingScript_2hp : NetworkBehaviour
{
   [SerializeField] GameObject rayCastLeftDown, rayCastRightDown;
   [SerializeField] LayerMask layerMask;
   bool isRayCastLeft,isRayCastRight;
   GameObject LeftUpTile, RightDownTile;
   public bool isHeld;
   bool isPlaced;
   [SerializeField] Vector3 startingPosition;
   AudioSource audioSource;

   void Awake()
   {
      isRayCastRight = false;
      isRayCastLeft = false;
      isHeld = false;
      audioSource = GetComponent<AudioSource>();
      GameEndEvent.current.onGameStart += OnGameStart;
   }
   void OnGameStart()
   {
      this.enabled = false;
   }
   void Start()
   {
      startingPosition = transform.position;
   }
   public void Update()
   {

      if(!IsOwner) return;
      
      if (isHeld)
      {
         if (rayCastLeftDown != null)
         {
            RayCastLeft();
         }
         if (rayCastRightDown != null)
         {
            RayCastRight();
         }
         if (Input.GetKeyDown(KeyCode.R))
            {
               transform.rotation *= Quaternion.Euler(0, 90, 0);
            }
      }

      if (isHeld == false)
      {
         if (isRayCastLeft && isRayCastRight && LeftUpTile != null && RightDownTile != null)
         {
            transform.position = new Vector3((LeftUpTile.transform.position.x + RightDownTile.transform.position.x) * (float)0.5, 2f, (LeftUpTile.transform.position.z + RightDownTile.transform.position.z) * (float)0.5 );
         }
         else
         {
            transform.position = startingPosition;
         }
      }
   }



   void RayCastLeft()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastLeftDown.transform.position, rayCastLeftDown.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
         LeftUpTile = hit.transform.gameObject;
         isRayCastLeft = true;
      }
      else
      {
         LeftUpTile = null;
         isRayCastLeft = false;
      }
   }
  
   void RayCastRight()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastRightDown.transform.position, rayCastLeftDown.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
         RightDownTile = hit.transform.gameObject;
         isRayCastRight = true;
      }
      else
      {
         RightDownTile = null;
         isRayCastRight = false;
      }
   }

   public void PlayAudio()
   {
      audioSource.Play();
   }
}
