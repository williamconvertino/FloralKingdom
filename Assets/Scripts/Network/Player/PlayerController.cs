using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(EntityAttack))]
public class PlayerController : NetworkBehaviour
{
    #region Initialization

    private EntityMovement _movement;
    private EntityAttack _attack;

    private void Awake()
    {
        _movement = GetComponent<EntityMovement>();
        _attack = GetComponent<EntityAttack>();
    }

    #endregion
    
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            _movement.Direction = inputData.Direction;
            if (inputData.Attack) _attack.Attack();
        }
    }
}
