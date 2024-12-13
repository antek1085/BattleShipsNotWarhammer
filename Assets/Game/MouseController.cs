using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MouseController : NetworkBehaviour
{
    Vector3 mousePosition;
    [SerializeField] LayerMask layerMask;
    int click;

    GameObject ship;
    Vector3 shipPosition;
    Plane plane;
    Ray ray;


    void Awake()
    {
        plane = new Plane(Vector3.up, this.transform.position);
        
    }
    public override void FixedUpdateNetwork()
    {
        mousePosition = Input.mousePosition;

        if (GetInput(out NetworkInputData data))
        {
            if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON))
            {
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
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
            
            
            /*if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask))
            {
                ship = hit.transform.gameObject; 
                ship.GetComponent<PlaceingScript>().isHeld = true;
            }
            else
            {
                if (ship != null)
                {
                    ship.GetComponent<PlaceingScript>().isHeld = false;
                    ship = null;
                }
            }*/
        }

        if (ship != null)
        {
            ray = Camera.main.ScreenPointToRay(mousePosition);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPostion = ray.GetPoint(distance);
                ship.transform.position = new Vector3(targetPostion.x, 16.5f, targetPostion.z);
            }
            
        }
    }
}
