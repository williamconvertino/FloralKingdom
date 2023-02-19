using System;
using Fusion;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float speed;
    
    #region Initialization

    private Rigidbody2D _rb2d;
    private EntitySpriteRenderer _entitySpriteRenderer;
    private EntityAnimator _entityAnimator;
    
    private Vector2 _direction;
    private bool _doAttack;

    public void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _entitySpriteRenderer = GetComponentInChildren<EntitySpriteRenderer>();
        _entityAnimator = GetComponentInChildren<EntityAnimator>();
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            _direction = inputData.Direction;
            _doAttack = inputData.Attack;
        }
    }
    private void Update()
    {
        UpdatePlayerStateStart();
        UpdateAction();
        UpdateMovement();
        UpdateDirection();
        UpdatePlayerStateEnd();
    }

    #endregion

    #region PlayerState

    private enum PlayerState
    {
        None,
        Idle,
        Move,
        Action
    }

    private PlayerState _playerState = PlayerState.None;

    private void UpdatePlayerStateStart()
    {
        
    }
    
    private void UpdatePlayerStateEnd()
    {
        if (_playerState == PlayerState.None)
        {
            _playerState = PlayerState.Idle;
            _entityAnimator.PlayAnimation(EntityAnimationState.Idle);
        }
    }
    
    #endregion
    
    #region Movement
    
    protected virtual void UpdateMovement()
    {
        if (BlockMovement()) return;
        
        _rb2d.velocity = _direction.normalized * speed;

        if (_direction.magnitude == 0)
        {
            _playerState = PlayerState.None;
        }
        else
        {
            _playerState = PlayerState.Move;
            _entityAnimator.PlayAnimation(EntityAnimationState.Move);
        }
    }

    protected virtual bool BlockMovement()
    {
        return false;
    }

    #endregion

    #region Direction
    
    private bool _flipX;
    protected virtual void UpdateDirection()
    {
        if (!AllowDirectionChange()) return;
        if (_direction.x > 0) _flipX = false;
        if (_direction.x < 0) _flipX = true;
        if (_flipX != _entitySpriteRenderer.FlipX) _entitySpriteRenderer.FlipX = _flipX;
    }

    protected virtual bool AllowDirectionChange()
    {
        return true;
    }
    
    #endregion
    
    #region Action
    protected virtual void UpdateAction()
    {
        if (BlockAction()) return;
    }

    protected virtual bool BlockAction()
    {
        return true;
    }

    #endregion

}
