using System;
using Fusion;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EntityActionManager))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] protected float speed;
    
    #region Initialization

    protected Rigidbody2D rb2d;
    protected EntityActionManager entityActionManager;
    protected EntitySpriteRenderer entitySpriteRenderer;
    protected EntityAnimator entityAnimator;
    
    protected Vector2 Direction;
    protected bool DoAttack;

    public void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        entityActionManager = GetComponent<EntityActionManager>();
        
        entitySpriteRenderer = GetComponentInChildren<EntitySpriteRenderer>();
        entityAnimator = GetComponentInChildren<EntityAnimator>();
        entityAnimator.OnAnimationComplete.AddListener(e => EndAction());
    }
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            Direction = inputData.Direction;
            DoAttack = inputData.Attack;
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

    protected enum PlayerState
    {
        None,
        Idle,
        Move,
        Action_Locked,
        Action_Unlocked
    }

    protected PlayerState playerState = PlayerState.None;

    private void UpdatePlayerStateStart()
    {
        
    }
    
    private void UpdatePlayerStateEnd()
    {
        if (playerState == PlayerState.None)
        {
            playerState = PlayerState.Idle;
            entityAnimator.PlayAnimation(EntityAnimationState.Idle);
        }
    }
    
    #endregion
    
    #region Movement

    protected bool DoMovement { set; get; } = true;
    protected bool FreezePosition { set; get; } = false;
    protected virtual void UpdateMovement()
    {
        if (!DoMovement) return;
        
        if (!FreezePosition) rb2d.velocity = Direction.normalized * speed;

        if (playerState == PlayerState.Action_Locked) return;

        if (Direction.magnitude == 0)
        {
            if (playerState == PlayerState.Move) playerState = PlayerState.None;
            return;
        }

        if (playerState == PlayerState.None || playerState == PlayerState.Idle)
        {
            playerState = PlayerState.Move;
            entityAnimator.PlayAnimation(EntityAnimationState.Move);
        }
    }

    #endregion

    #region Direction

    protected bool DoDirectionChange { set; get; } = true;
    protected bool _flipX;
    protected virtual void UpdateDirection()
    {
        if (!DoDirectionChange) return;
        if (Direction.x > 0) _flipX = false;
        if (Direction.x < 0) _flipX = true;
        if (_flipX != entitySpriteRenderer.FlipX) entitySpriteRenderer.FlipX = _flipX;
    }

    #endregion
    
    #region Action
    protected virtual void UpdateAction()
    {
        
    }

    public void EndAction()
    {
        playerState = PlayerState.None;
    }
    
    #endregion
    
}
