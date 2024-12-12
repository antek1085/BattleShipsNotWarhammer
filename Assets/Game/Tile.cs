using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
   [SerializeField] Material newMaterial;
   Material actualMaterial;
   [SerializeField] GameObject missle;
   public bool isRaycasted; 
   int index;
   [SerializeField] private LayerMask layerMask;
   
   

   void Awake()
   {
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
         Debug.Log("Spada");
         Instantiate(missle, new Vector3(this.transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.Euler(0, 0, 0));
   }

   public void TileHit()
   {
      RaycastHit hit;
      if (Physics.Raycast(this.transform.position, transform.transform.up, out hit, Mathf.Infinity, layerMask))
      {
         hit.transform.GetComponent<PlaceingScript>().HP -= 1;
      }
   }

}
