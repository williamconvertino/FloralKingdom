using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    #region Initialization
    
    private Rigidbody2D _rb2d;
    private EntityAnimator _entityAnimator;
    private EntitySpriteRenderer _entitySpriteRenderer;
        
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _entityAnimator = GetComponentInChildren<EntityAnimator>();
        _entitySpriteRenderer = GetComponentInChildren<EntitySpriteRenderer>();
    }

    #endregion

    #region Movement
    
    [SerializeField] private float speed;
    public Vector2 Direction { set; get; }
    
    private bool _flipX;
    
    private void Update()
    {
        if (Direction.x > 0) _flipX = false;
        if (Direction.x < 0) _flipX = true;
        if (_flipX != _entitySpriteRenderer.FlipX) _entitySpriteRenderer.FlipX = _flipX;
        
        _rb2d.velocity = Direction.normalized * speed;

        if (Direction.magnitude == 0)
        {
            _entityAnimator.PlayAnimation(EntityAnimationState.Idle);   
        }
        else
        {
            _entityAnimator.PlayAnimation(EntityAnimationState.Move); 
        }
        
    }

    #endregion
}
