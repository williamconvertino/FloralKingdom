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

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            _networkPlayerController.Move(networkInputData.MoveDirection);
            _networkPlayerController.Attack(networkInputData.Attack1);
        }
    }
}
