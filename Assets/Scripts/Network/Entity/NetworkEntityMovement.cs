using Fusion;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
public class NetworkEntityMovement : NetworkBehaviour
{
    #region Initialization

    private EntitySpriteRenderer _networkSpriteRenderer;
    private EntityMovement _entityMovement;

    private void Awake()
    {
        _networkSpriteRenderer = GetComponentInChildren<EntitySpriteRenderer>();
        _entityMovement = GetComponent<EntityMovement>();
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