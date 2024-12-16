using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraChange : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        switch (IsHost)
        {
            
            case true:
                switch (OwnerClientId)
                {
                    case 0:
                        GetComponent<Camera>().targetDisplay = 0;
                        break;
                    case 1:
                        GetComponent<Camera>().targetDisplay = 3;
                        break;
                }
                break;
            
            case false:
                switch (OwnerClientId)
                {
                    case 0:
                        GetComponent<Camera>().targetDisplay = 3;
                        break;
                    case 1:
                        GetComponent<Camera>().targetDisplay = 0;
                        break;
                }
                break;
        }
    }
}
