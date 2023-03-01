using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EntityAnimator))]
[RequireComponent(typeof(EntityModel))]
public abstract class EntityController : NetworkBehaviour
{
    protected Vector2 Direction = Vector2.zero;

    [HideInInspector] public bool LockAction;
    [HideInInspector] public bool LockMovement;
    [HideInInspector] public bool LockDirection;

    #region Initialization

    protected Rigidbody2D RB2D;
    protected EntityModel EntityModel;
    protected EntityAnimator EntityAnimator;
    
    protected EntityActionManager EntityActionManager;
    
    protected virtual void Awake()
    {
        RB2D = GetComponent<Rigidbody2D>();
        EntityModel = GetComponentInChildren<EntityModel>();
        
        EntityAnimator = GetComponentInChildren<EntityAnimator>();
        EntityAnimator.OnActionLockComplete.AddListener(e => UnlockAction());
        EntityAnimator.OnMoveLockComplete.AddListener(e => UnlockMovement());
        
        EntityActionManager = GetComponent<EntityActionManager>();
    }

    #endregion

    #region Locks

    protected virtual void UnlockAction()
    {
        LockAction = false;
    }
    protected virtual void UnlockMovement()
    {
        LockMovement = false;
    }
    protected virtual void UnlockDirection()
    {
        LockDirection = false;
    }
    #endregion
    
    #region Update

    public override void FixedUpdateNetwork()
    {
        UpdateStart();
        if (!LockAction) UpdateAction();
        if (!LockMovement) UpdateMovement();
        if (!LockDirection) UpdateDirection();
        UpdateEnd();
    }

    protected virtual void UpdateStart(){}
    protected virtual void UpdateAction() {} 
    protected virtual void UpdateMovement() {}
    protected virtual void UpdateEnd() {}

    #endregion

    #region Direction
    
    protected bool FlipX;
    protected void UpdateDirection()
    {
        if (Direction.x > 0) FlipX = false;
        if (Direction.x < 0) FlipX = true;
        if (FlipX != EntityModel.FlipX) EntityModel.FlipX = FlipX;
    }
    
    #endregion
}