using Fusion;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class NetworkEntityMovement : NetworkBehaviour
{
    #region Initialization

    private NetworkSpriteRenderer _networkSpriteRenderer;
    private EntityMovement _entityMovement;

    private void Awake()
    {
        _networkSpriteRenderer = GetComponentInChildren<NetworkSpriteRenderer>();
        _entityMovement = GetComponent<EntityMovement>();
        _entityMovement.FreezeSpriteDirection = true;
    }

    #endregion

    #region FlipX
    private bool _flipX;
    private void Update()
    {
        if (_entityMovement.Direction.x > 0) _flipX = false;
        if (_entityMovement.Direction.x < 0) _flipX = true;
        if (_flipX != _networkSpriteRenderer.FlipX) _networkSpriteRenderer.FlipX = _flipX;
    }
    #endregion
    
}