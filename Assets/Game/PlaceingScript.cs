using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceingScript : MonoBehaviour
{
   [SerializeField] GameObject rayCastLeft, rayCastMiddle, rayCastRight;
   [SerializeField] LayerMask layerMask;
   bool isHeld;

   void Awake()
   {
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
      }
   }



   void RayCastLeft()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastLeft.transform.position, rayCastLeft.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
      }
   }

   void RayCastMiddle()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastMiddle.transform.position, rayCastMiddle.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
      }
   }
   void RayCastRight()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastRight.transform.position, rayCastMiddle.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
      }
   }
   
}
