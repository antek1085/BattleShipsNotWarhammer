using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
   [SerializeField] Material newMaterial;
   Material actualMaterial;
   [SerializeField] GameObject missle;
   public bool isRaycasted; 
   int index;
   
   

   void Awake()
   {
      index = transform.GetSiblingIndex();
      ShootMissleEvent.current.onMissleShoot += OnMissleShoot;
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

   void OnMissleShoot(int indexEvent)
   {
      if (indexEvent == index)
      {
         Debug.Log("Spada");
         Instantiate(missle, new Vector3(this.transform.position.x, transform.position.y + 10f, transform.position.z), Quaternion.Euler(0, 0, 0));
      }
   }

   public void TileHit()
   {
      Debug.Log(index + " Tile");
   }

}
