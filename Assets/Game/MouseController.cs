using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MouseController : NetworkBehaviour
{
    Vector3 mousePosition;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Camera _camera;
    int click;

    GameObject ship;
    Vector3 shipPosition;
    Plane plane;
    Ray ray;



    void Awake()
    {
        GameEndEvent.current.onGameStart += OnGameStart;
    }

    void Start()
    {
        plane = new Plane(Vector3.up, this.transform.position);
        
    }
    void OnGameStart()
    {
        this.enabled = false;
    }
    public void Update()
    {
        if (!IsOwner)return;

        mousePosition = Input.mousePosition;

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            switch (click)
            {
                case 0: 
                    if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask))
                    {
                        ship = hit.transform.gameObject; 
                        
                        ship.GetComponent<PlaceingScript>().isHeld = true;
                        click = 1;
                    }
                    break;
                case 1:
                    if (ship != null && click == 1) 
                    {
                        
                        ship.GetComponent<PlaceingScript>().isHeld = false;
                        ship = null;
                        click = 0;
                    }
                    break;
            }
        }

        if (ship != null)
        {
            ray = _camera.ScreenPointToRay(mousePosition);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPostion = ray.GetPoint(distance);
                ship.transform.position = new Vector3(targetPostion.x, 3f, targetPostion.z);
            }
            
        }
    }
}
