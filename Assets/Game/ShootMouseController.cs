using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMouseController : MonoBehaviour
{
    Ray ray;
    Vector3 mousePosition;
    [SerializeField] LayerMask layerMask;


    void Update()
    {
        mousePosition = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log("HIT");
                hit.transform.GetComponent<TargetableTile>().ShootMissle();
            }
        }
    }
}
