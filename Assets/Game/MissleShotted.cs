using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleShotted : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Placable Tile")
        {
            other.GetComponent<Tile>().TileHit();
            Destroy(gameObject);
        }
    }
}
