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
    }

    private void Update()
    {
        UpdateAnimation();
    }
    #endregion

    #region Play Animation

    [Networked(OnChanged = nameof(OnQueuedAnimationState))]
    [HideInInspector]
    public EntityAnimationState NewAnimationState { set; get; }

    private EntityAnimationState _queuedAnimationState = EntityAnimationState.None;
    private EntityAnimationState _currentAnimationState = EntityAnimationState.None;
    
    public static void OnQueuedAnimationState(Changed<EntityAnimator> changed)
    {
        changed.Behaviour.RunAnimator(changed.Behaviour.NewAnimationState.ToString());
    }
    
    public void PlayAnimation(EntityAnimationState animationState)
    {
        _queuedAnimationState = animationState;
    }

    private void LoadAnimationState(EntityAnimationState animationState)
    {
        _currentAnimationState = animationState;
        NewAnimationState = _currentAnimationState;
    }

    #endregion

    #region Update and Play Animation
    public void UpdateAnimation()
    {
        if (_currentAnimationState == EntityAnimationState.None && _queuedAnimationState == EntityAnimationState.None)
        {
            LoadAnimationState(EntityAnimationState.Idle);
            return;
        }
        if (_queuedAnimationState == EntityAnimationState.None) return;

        if (_queuedAnimationState != _currentAnimationState)
        {
            LoadAnimationState(_queuedAnimationState);
        }

        _queuedAnimationState = EntityAnimationState.None;
        
    }

    public void RunAnimator(String animationName)
    {
        _animator.Play(animationName);
    }
    
    #endregion

}
