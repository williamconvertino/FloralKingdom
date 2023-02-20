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

    private void Update()
    {
        UpdateAnimation();
    }
    #endregion

    #region Play Animation
    
    private EntityAnimationState _queuedAnimationState = EntityAnimationState.None;
    public void PlayAnimation(EntityAnimationState animationState)
    {
        _queuedAnimationState = animationState;
    }

    #endregion

    #region Load and Run Animation
    
    [Networked(OnChanged = nameof(OnNewAnimationState))]
    [HideInInspector]
    public EntityAnimationState NewAnimationState { set; get; }
    private void UpdateAnimation()
    {
        if (_queuedAnimationState == EntityAnimationState.None) return;
        NewAnimationState = _queuedAnimationState;
        _queuedAnimationState = EntityAnimationState.None;
    }
    public static void OnNewAnimationState(Changed<EntityAnimator> changed)
    {
        changed.Behaviour.RunAnimator(changed.Behaviour.NewAnimationState.ToString());
    }
    public void RunAnimator(String animationName)
    {
        _animator.Play(animationName);
    }

    #endregion
}
