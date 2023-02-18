using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    #region Initialization
    
    private Rigidbody2D _rb2d;
    private EntityAnimator _animator;
    private SpriteRenderer _spriteRenderer;
        
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<EntityAnimator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    #endregion

    #region Movement
    
    [SerializeField] private float speed;
    public Vector2 Direction { set; get; }
    private void Update()
    {
        
        UpdateSpriteDirection();
        
        _rb2d.velocity = Direction.normalized * speed;
        
        if (Direction.magnitude == 0)
        {
            UpdateAnimation(EntityAnimator.AnimationState.Idle);
        }
        else
        {
            UpdateAnimation(EntityAnimator.AnimationState.Move);
        }
    }

    #endregion

    #region UpdateSpriteDirection

    public bool FreezeSpriteDirection { set; get; }
    private bool _flipX;
    
    protected void UpdateSpriteDirection()
    {
        if (FreezeSpriteDirection) return;
        if (Direction.x > 0) _flipX = false;
        if (Direction.x < 0) _flipX = true;
        _spriteRenderer.flipX = _flipX;
    }

    #endregion

    #region UpdateAnimation

    protected virtual void UpdateAnimation(EntityAnimator.AnimationState animationState)
    {
        _animator.PlayAnimation(animationState);
    }

    #endregion
}
