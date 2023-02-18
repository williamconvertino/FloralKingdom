using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    
    private EntityAnimator _animator;
    private bool _attack;
    private void Start()
    {
        _animator = GetComponentInChildren<EntityAnimator>();
    }

    private void Update()
    {
        // if (_attack && !_animator.IsAttacking()) _animator.PlayAttackAnimation("Headbutt");
        // _attack = false;
    }

    public void Attack()
    {
        _attack = true;
    }
}
