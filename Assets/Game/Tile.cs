using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
   [SerializeField] Material newMaterial;
   Material actualMaterial;
   public bool isRaycasted;

   void Awake()
   {
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

}
