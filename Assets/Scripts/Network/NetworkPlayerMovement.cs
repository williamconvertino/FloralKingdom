using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class NetworkPlayerMovement : NetworkBehaviour
{
    private NetworkPlayerController _networkPlayerController;

    private void Awake()
    {
        _networkPlayerController = GetComponent<NetworkPlayerController>();
    }

    public void FixedUpdate()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            
        }
    }
}
