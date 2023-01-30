using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Rigidbody2D _rb2d;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    private Vector2 _direction;
    private int _lastXDirection = 1;
    
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    #region Update

    private void Update()
    {
        UpdateMovement();
        UpdateAttack();
    }

    #endregion

    #region Movement

    private void UpdateMovement()
    {
        _direction = PlayerInput.GetMoveDirection().normalized;
        _rb2d.velocity = _direction * speed;

        if (_direction.x == 0.0f)
        {
            PlayAnimation("Idle");
        }
        else
        {
            PlayAnimation("Move");
            _lastXDirection = Math.Sign(_direction.x);
        }
        _spriteRenderer.flipX = _lastXDirection < 0;
    }

    #endregion

    #region Attack

    private bool _isAttacking = false;
    private void UpdateAttack()
    {
        if (PlayerInput.GetAttackPrimary())
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        if (_isAttacking)
        {
            return;
        }
        print("Starting");
        PlayAnimation("Headbutt");
        _isAttacking = true;
    }

    public void FinishAttack()
    {
        _isAttacking = false;
        
        print("Finished");
    }

    #endregion
    
    #region Animator

    private string _currentAnimation;
    private void PlayAnimation(string animationName)
    {
        if (animationName == _currentAnimation || _isAttacking) return;
        
        _animator.Play(animationName);

        _currentAnimation = animationName;
    }

    #endregion

}
