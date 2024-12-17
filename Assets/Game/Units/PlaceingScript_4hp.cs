using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class PlaceingScript_4hp : NetworkBehaviour
{
   [SerializeField] GameObject rayCastLeftDown, rayCastLeftUp, rayCastRightDown,rayCastRightUp;
   [SerializeField] LayerMask layerMask;
   bool isRayCastLeft, isRayCastMiddle, isRayCastRight, isRayCastRightUp;
   GameObject LeftUpTile, RightDownTile;
   public bool isHeld;
   public bool isPlaced;
   [SerializeField] Vector3 startingPosition;

   void Awake()
   {
      isRayCastRight = false;
      isRayCastLeft = false;
      isRayCastMiddle = false;
      isRayCastRightUp = false;
      isHeld = false;

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
         if (rayCastLeftUp != null)
         {
            RayCastMiddle();
         }
         if (rayCastRightDown != null)
         {
            RayCastRight();
         }
         if (rayCastRightUp != null)
         {
            RayCastRightUp();
         }
         
         if (Input.GetKeyDown(KeyCode.R))
         { 
            transform.rotation *= Quaternion.Euler(0, 90, 0);
         }
      }

      if (isHeld == false)
      {
         if (isRayCastLeft == true && isRayCastMiddle == true && isRayCastRight == true && LeftUpTile != null)
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
         isRayCastLeft = true;
      }
      else
      {
         isRayCastLeft = false;
      }
   }
   void RayCastRightUp()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastRightUp.transform.position, rayCastRightUp.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
         
         isRayCastRightUp = true;
      }
      else
      {
         isRayCastRightUp = false;
      }
   }

   void RayCastMiddle()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastLeftUp.transform.position, rayCastLeftUp.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
         LeftUpTile = hit.transform.gameObject;
         isRayCastMiddle = true;
      }
      else
      {
         LeftUpTile = null;
         isRayCastMiddle = false;
      }
   }
   void RayCastRight()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastRightDown.transform.position, rayCastRightDown.transform.up * -1, out hit, Mathf.Infinity, layerMask))
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
}
