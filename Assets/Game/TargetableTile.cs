using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TargetableTile : NetworkBehaviour
{
    int index;
    int clientId;
    public bool isActive;
    
    
    public bool isRaycasted;
    [SerializeField] Material newMaterial;
    Material actualMaterial;
    
    
    bool didHit;
    Material _material;
    [SerializeField] Material hitTarget;
    [SerializeField] Material missTarget;
    void Awake()
    {
        isActive = true;
        index = transform.GetSiblingIndex();
        _material = gameObject.GetComponent<MeshRenderer>().material;
        actualMaterial = GetComponent<MeshRenderer>().material;
    }
    public void ShootMissle()
    {
        if(!IsOwner) return;
        if (isActive)
        {
            clientId = (int)OwnerClientId;
            ShootMissleEvent.current.MissleShoot(index,clientId);  
        }
    }

    public void CheckShoot(bool didShootHit)
    {
        isActive = false;
        Debug.Log(transform.name+": " + OwnerClientId);
        switch (didShootHit)
        {
            case true:
                gameObject.GetComponent<MeshRenderer>().material = hitTarget;
                break;
            case false:
                gameObject.GetComponent<MeshRenderer>().material = missTarget;
                break;
        }
        Debug.Log(_material);
    }

    void Update()
    {
        if (isActive)
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
}
