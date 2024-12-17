using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaceingScript_1hp : NetworkBehaviour
{
   [SerializeField] GameObject rayCastMiddle;
   [SerializeField] LayerMask layerMask;
   bool isRayCastMiddle;
   GameObject MiddleTile, RightDownTile;
   public bool isHeld;
   [SerializeField] Vector3 startingPosition;

   void Awake()
   {
      isRayCastMiddle = false;
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
         if (rayCastMiddle != null)
         {
            RayCastMiddle();
         }
         
         if (Input.GetKeyDown(KeyCode.R))
         {
            transform.rotation *= Quaternion.Euler(0, 90, 0);
         }
      }

      if (isHeld == false)
      {
         if (isRayCastMiddle && MiddleTile != null)
         {
            Debug.Log(MiddleTile.name);
            transform.position = new Vector3((MiddleTile.transform.position.x) * (float)0.5, 2f, (MiddleTile.transform.position.z) * (float)0.5 );
            Debug.Log(transform.position + "co jest");
         }
         else
         {
            transform.position = startingPosition;
         }
      }
   }



   void RayCastMiddle()
   {
      RaycastHit hit;
      if (Physics.Raycast(rayCastMiddle.transform.position, rayCastMiddle.transform.up * -1, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<Tile>().isRaycasted = true;
         MiddleTile = hit.transform.gameObject;
         Debug.Log(MiddleTile.transform.position);
         isRayCastMiddle = true;
      }
      else
      {
         Debug.Log(false);
         MiddleTile = null;
         isRayCastMiddle = false;
      }
   }
   
}
