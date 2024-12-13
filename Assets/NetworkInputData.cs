using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public struct NetworkInputData : INetworkInput
{
    public const byte MOUSEBUTTON = 1;
    public const byte KEYBOARD_R = 2;
    
    public Vector3 direction;
    public NetworkButtons buttons;
}
