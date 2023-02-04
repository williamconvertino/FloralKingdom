using System;
using UnityEngine;
using Fusion;
public class NetworkInputHandler : MonoBehaviour
{
    private Vector2 _moveDirection;
    private bool _attack1;

    private void Update()
    {
        _moveDirection = PlayerInput.GetMoveDirection().normalized;
        _attack1 = PlayerInput.GetAttackPrimary();
    }

    public NetworkInputData GetNetworkInputData()
    {
        return new NetworkInputData()
        {
            MoveDirection = _moveDirection,
            Attack1 = _attack1
        };
    }
}
