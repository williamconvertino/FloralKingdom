using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class EntityAction : MonoBehaviour
{
    #region Initialization
    private EntityAnimator _animator;
    private void Start()
    {
        _animator = GetComponentInChildren<EntityAnimator>();
    }
    #endregion
    
    public bool DoAttack { set; get; }
    private void Update()
    {
        if (DoAttack) _animator.PlayAnimation(EntityAnimationState.Headbutt);
        DoAttack = false;
    }
}
