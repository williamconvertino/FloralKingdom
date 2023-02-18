using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(EntityAction))]
public class PlayerController : NetworkBehaviour
{
    #region Initialization

    private EntityMovement _entityMovement;
    private EntityAction _entityAction;

    private void Awake()
    {
        _entityMovement = GetComponent<EntityMovement>();
        _entityAction = GetComponent<EntityAction>();
    }

    #endregion
    
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            _entityMovement.Direction = inputData.Direction;
            _entityAction.DoAttack = inputData.Attack;
        }
    }
}
