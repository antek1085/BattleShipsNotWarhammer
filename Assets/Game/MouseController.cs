using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MouseController : NetworkBehaviour
{
    Vector3 mousePosition;
    [SerializeField] LayerMask layerMask2hp,layerMask4hp,layerMask1hp;
    [SerializeField] Camera _camera;
    int click;

    GameObject ship;
    Vector3 shipPosition;
    Plane plane;
    Ray ray;
    int howMuchHp;



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
                    if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask4hp))
                    {
                        howMuchHp = 4;
                        ship = hit.transform.gameObject; 
                        
                        ship.GetComponent<PlaceingScript_4hp>().isHeld = true; 
                        click = 1;
                        break;
                    }
                    if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask2hp))
                    {
                        howMuchHp = 2;
                        ship = hit.transform.gameObject; 
                        
                        ship.GetComponent<PlaceingScript_2hp>().isHeld = true; 
                        click = 1;
                        break;
                    }
                    if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask1hp))
                    {
                        howMuchHp = 1;
                        ship = hit.transform.gameObject; 
                        
                        ship.GetComponent<PlaceingScript_1hp>().isHeld = true; 
                        click = 1;
                        break;
                    }
                    break;
                case 1:
                    switch (howMuchHp)
                    {
                        case 1:
                            if (ship != null && click == 1) 
                            {
                        
                                ship.GetComponent<PlaceingScript_1hp>().isHeld = false;
                                ship = null;
                                click = 0;
                            }
                            break;
                        case 2:
                            if (ship != null && click == 1) 
                            {
                        
                                ship.GetComponent<PlaceingScript_2hp>().isHeld = false;
                                ship = null;
                                click = 0;
                            }
                            break;
                        case 4:
                            if (ship != null && click == 1) 
                            {
                        
                                ship.GetComponent<PlaceingScript_4hp>().isHeld = false;
                                ship = null;
                                click = 0;
                            }
                            break;
                        default:
                            break;
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
