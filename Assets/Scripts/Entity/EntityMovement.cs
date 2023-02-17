using System.Collections;
using System.Collections.Generic;
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
        _rb2d.velocity = Direction.normalized * speed;

        if (Direction.x > 0) _spriteRenderer.flipX = false;
        if (Direction.x < 0) _spriteRenderer.flipX = true;
        
        if (Direction.magnitude == 0)
        {
            _animator.PlayAnimation(EntityAnimator.AnimationState.Idle);
        }
        else
        {
            _animator.PlayAnimation(EntityAnimator.AnimationState.Move);
        }
    }

    #endregion
}
