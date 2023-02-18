using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : NetworkBehaviour
{
    
    #region Initialization and Update
    private Animator _animator;
    private void Start ()
    {
        _animator = GetComponent<Animator>();
        AnimationEventDispatcher eventDispatcher = gameObject.AddComponent<AnimationEventDispatcher>();
        eventDispatcher.OnAnimationComplete.AddListener(e => _currentAnimationState = AnimationState.None);
    }

    private void Update()
    {
        UpdateAnimation();
    }
    #endregion

    #region Animation State

    public enum AnimationState
        {
            None,
            Idle,
            Move,
            Action
        }
    #endregion

    [Networked(OnChanged = nameof(OnQueuedAnimationState))]
    [HideInInspector]
    public AnimationState NewAnimationState { set; get; }

    private AnimationState _queuedAnimationState = AnimationState.None;
    private AnimationState _currentAnimationState = AnimationState.None;
    
    public static void OnQueuedAnimationState(Changed<EntityAnimator> changed)
    {
        changed.Behaviour.RunAnimator(changed.Behaviour.NewAnimationState.ToString());
    }
    
    public void PlayAnimation(AnimationState animationState)
    {
        _queuedAnimationState = animationState;
    }

    public void UpdateAnimation()
    {
        if (_currentAnimationState == AnimationState.None && _queuedAnimationState == AnimationState.None)
        {
            SetAnimationState(AnimationState.Idle);
            return;
        }
        if (_queuedAnimationState == AnimationState.None) return;

        if (_queuedAnimationState != _currentAnimationState)
        {
            SetAnimationState(_queuedAnimationState);
        }

        _queuedAnimationState = AnimationState.None;
        
    }

    public void RunAnimator(String animationName)
    {
        _animator.Play(animationName);
    }
    
    private void SetAnimationState(AnimationState animationState)
    {
        _currentAnimationState = animationState;
        NewAnimationState = _currentAnimationState;
    }
    

}
