using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EntityAnimator))]
public class PlayerController : MonoBehaviour
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
    private void UpdateMovement()
    {
        _direction = PlayerInput.GetMoveDirection().normalized;
        _rb2d.velocity = _direction * speed;

        if (_direction.x == 0.0f)
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
    private void UpdateAttack()
    {
        if (PlayerInput.GetAttackPrimary())
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
