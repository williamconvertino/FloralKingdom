using System;
using UnityEngine;
using Fusion;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EntityAnimator))]
public class NetworkPlayerController : NetworkTransform
{
    [SerializeField] private float speed;
    
    private Rigidbody2D _rb2d;
    private SpriteRenderer _spriteRenderer;
    private EntityAnimator _entityAnimator;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _entityAnimator = GetComponent<EntityAnimator>();
    }

    #region Update

    private void Update()
    {
        UpdateMovement();
        UpdateAttack();
    }

    #endregion

    #region Movement
    
    private Vector2 _direction;
    private int _lastXDirection = 1;

    public void Move(Vector2 direction)
    {
        _direction = direction.normalized;
    }
    private void UpdateMovement()
    {
        
        _rb2d.velocity = _direction * speed;
        if (_direction.magnitude == 0.0f)
        {
            _entityAnimator.PlayAnimation(EntityAnimator.AnimationState.Idle);
        }
        else
        {
            _entityAnimator.PlayAnimation(EntityAnimator.AnimationState.Move);
            _lastXDirection = Math.Sign(_direction.x);
        }
        _spriteRenderer.flipX = _lastXDirection < 0;
    }

    #endregion

    #region Attack

    private bool _attack1;

    public void Attack(bool attack1)
    {
        _attack1 = attack1;
    }
    private void UpdateAttack()
    {
        if (_attack1)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (_entityAnimator.IsAttacking()) return;
            
        _entityAnimator.PlayAttackAnimation("Headbutt");
    }

    #endregion
    

}