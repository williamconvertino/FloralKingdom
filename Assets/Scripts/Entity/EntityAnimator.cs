using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : NetworkBehaviour
{
    #region Initialization and Update

    public UnityAnimationEvent OnAnimationComplete;

    private Animator _animator;
    private AnimationEventDispatcher _eventDispatcher;
    private void Awake ()
    {
        _animator = GetComponent<Animator>();
        _eventDispatcher = GetComponent<AnimationEventDispatcher>();
        _eventDispatcher.OnAnimationComplete.AddListener(e => OnAnimationComplete?.Invoke(e));
    }
    #endregion

    #region CurrentAnimationState

    [Networked(OnChanged = nameof(OnNewAnimationState))]
    [HideInInspector]
    public EntityAnimationState CurrentAnimationState { set; get; }
    public static void OnNewAnimationState(Changed<EntityAnimator> changed)
    {
        changed.Behaviour.RunAnimator(changed.Behaviour.CurrentAnimationState);
    }
    #endregion
    
    #region Play Animation
    
    private EntityAnimationState _queuedAnimationState = EntityAnimationState.None;
    public void PlayAnimation(EntityAnimationState animationState)
    {
        _queuedAnimationState = animationState;
    }

    #endregion
    
    #region Update animation
    private void Update()
    {
        if (_queuedAnimationState == EntityAnimationState.None) return;
        CurrentAnimationState = _queuedAnimationState;
        _queuedAnimationState = EntityAnimationState.None;
    }
    #endregion

    #region Run Animator
    public void RunAnimator(EntityAnimationState animationState)
    {
        if (animationState == EntityAnimationState.None) return;
        _animator.Play(animationState.ToString());
    }
    #endregion
}
