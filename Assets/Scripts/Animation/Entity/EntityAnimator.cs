using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : NetworkBehaviour
{
    #region Initialization

    public UnityAnimationEvent OnAnimationComplete;
    public UnityAnimationEvent OnActionLockComplete;
    public UnityAnimationEvent OnMoveLockComplete;

    private Animator _animator;
    private AnimationEventDispatcher _eventDispatcher;
    private bool _onAnimationEnd;
    private void Awake ()
    {
        _animator = GetComponent<Animator>();
        _eventDispatcher = GetComponent<AnimationEventDispatcher>();
        _eventDispatcher.OnAnimationComplete.AddListener(e => { OnAnimationComplete?.Invoke(e); _onAnimationEnd = true; });
    }
    #endregion

    #region Animation Events

    public void TriggerActionLockComplete()
    {
        OnActionLockComplete.Invoke(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    }

    public void TriggerMoveLockComplete()
    {
        OnActionLockComplete.Invoke(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
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

    [Networked(OnChanged = nameof(OnNewAnimationState))]
    [HideInInspector]
    public NetworkBool ForceAnimationPlayToggle { set; get; }

    #endregion
    
    #region Play Animation
    
    private EntityAnimationState _queuedAnimationState = EntityAnimationState.None;
    public void PlayAnimation(EntityAnimationState animationState)
    {
        _queuedAnimationState = animationState;
    }

    #endregion
    
    #region Update Animation
    private void Update()
    {
        if (_queuedAnimationState == EntityAnimationState.None) return;

        if (CurrentAnimationState.IsAction() && CurrentAnimationState == _queuedAnimationState)
        {
            ForceAnimationPlayToggle = !ForceAnimationPlayToggle;
        }
        else
        {
            CurrentAnimationState = _queuedAnimationState;
        }
        
        _queuedAnimationState = EntityAnimationState.None;
    }
    #endregion

    #region Run Animator
    public void RunAnimator(EntityAnimationState animationState)
    {
        if (animationState == EntityAnimationState.None) return;
        _animator.Play(animationState.ToString(), 0,0.0f);
    }
    #endregion
}
