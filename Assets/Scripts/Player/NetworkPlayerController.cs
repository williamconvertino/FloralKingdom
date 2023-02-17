using System;
using Fusion;
using UnityEngine;


public class NetworkPlayerController : NetworkBehaviour
{
    private EntityMovement _movement;
    private EntityAttack _attack;

    private void Awake()
    {
        _movement = GetComponent<EntityMovement>();
        _attack = GetComponent<EntityAttack>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            _movement.Direction = inputData.Direction;
            if (inputData.Attack) _attack.Attack();
        }
    }
}
