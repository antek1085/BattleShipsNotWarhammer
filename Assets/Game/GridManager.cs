using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
   [SerializeField] private int width, height;

   [SerializeField] private Tile tilePrefab;

   private void Start()
   {
      GeneratorGrid();
   }

   void GeneratorGrid()
   {
      for (int x = 0; x < width; x++)
      {
         for (int y = 0; y < height; y++)
         {
            var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
            spawnedTile.name = $"Tile + {x}{y}";
         }
      }
   }
}
