using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ShootMouseController : NetworkBehaviour
{
    Ray ray;
    Vector3 mousePosition;
    [SerializeField] LayerMask layerMask;
    [Networked] private NetworkButtons _buttonsPrevious { get; set; }

    public override void FixedUpdateNetwork()
    {
        mousePosition = Input.mousePosition;

        if (GetInput(out NetworkInputData data))
        {
            /*if (data.buttons.WasPressed(_buttonsPrevious,NetworkInputData.MOUSEBUTTON))
            {
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit hit;
                if(Physics.Raycast( ray, out hit, Mathf.Infinity, layerMask))
                {
                    Debug.Log("HIT");
                    hit.transform.GetComponent<TargetableTile>().ShootMissle();
                }

                _buttonsPrevious = data.buttons;
            }   */
            if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON))
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
}
