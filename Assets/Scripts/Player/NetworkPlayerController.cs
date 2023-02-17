using System;
using Fusion;
using UnityEngine;


public class NetworkPlayerController : NetworkBehaviour
{
    private EntityMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<EntityMovement>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            _movement.Direction = inputData.Direction;
        }
    }
}
