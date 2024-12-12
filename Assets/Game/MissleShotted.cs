using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MissleShotted : NetworkBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Placable Tile")
        {
            other.GetComponent<Tile>().TileHit();
            Runner.Despawn(Object);
        }
    }
}
