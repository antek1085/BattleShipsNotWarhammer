using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceingScript : MonoBehaviour
{
   [SerializeField] GameObject rayCastLeft, rayCastMiddle, rayCastRight;
   [SerializeField] LayerMask layerMask;
   bool isRayCastLeft, isRayCastMiddle, isRayCastRight;
   GameObject middleTile;
   public bool isHeld;
   [SerializeField] Vector3 startingPosition;

   void Awake()
   {
      isRayCastRight = false;
      isRayCastLeft = false;
      isRayCastMiddle = false;
      startingPosition = transform.position;
      isHeld = true;
   }
   void Update()
   {
      if (isHeld)
      {
         if (rayCastLeft != null)
         {
            RayCastLeft();
         }
         if (rayCastMiddle != null)
         {
            RayCastMiddle();
         }
         if (rayCastRight != null)
         {
            RayCastRight();
         }

         if (Input.GetKeyUp(KeyCode.R))
         {
            transform.rotation *= Quaternion.Euler(0,90,0);
            Debug.Log("1");
            
         }
      }

      if (isHeld == false)
      {
         if (isRayCastLeft == true && isRayCastMiddle == true && isRayCastRight == true && middleTile != null)
         {
            transform.position = new Vector3(middleTile.transform.position.x, 15f, middleTile.transform.position.z);
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
      if (Physics.Raycast(rayCastLeft.transform.position, rayCastLeft.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
         isRayCastLeft = true;
      }
      else
      {
         isRayCastLeft = false;
      }
   }

   void RayCastMiddle()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastMiddle.transform.position, rayCastMiddle.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
         middleTile = hit.transform.gameObject;
         isRayCastMiddle = true;
      }
      else
      {
         middleTile = null;
         isRayCastMiddle = false;
      }
   }
   void RayCastRight()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastRight.transform.position, rayCastMiddle.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
         isRayCastRight = true;
      }
      else
      {
         isRayCastRight = false;
      }
   }
   
}
