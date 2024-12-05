using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    Vector3 mousePosition;
    [SerializeField] LayerMask layerMask;

    GameObject ship;
    public Plane plane;
    Ray ray;
    void Update()
    {
        mousePosition = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask))
            {
                ship = hit.transform.gameObject;
                Debug.Log(ship);
            }
        }

        if (ship != null)
        {
            if (plane.Raycast(ray, out float distance))
            {
                ship.transform.position = ray.GetPoint(distance);

                var transformPosition = ship.transform.position;
                transformPosition.y = 16.5f;
            }   
        }
    }
}
