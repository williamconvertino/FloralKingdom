using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : NetworkBehaviour
{
    
    #region Initialization and Update
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
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
            Attack
        }
    #endregion

    #region Play Animation
    
    private AnimationState _currentAnimationState;

    [Networked(OnChanged = nameof(OnQueuedAnimationState))]
    [HideInInspector]
    public AnimationState QueuedAnimationState { set; get; } = AnimationState.Idle;

    public static void OnQueuedAnimationState(Changed<EntityAnimator> changed)
    {
        changed.Behaviour.PlayAnimation(changed.Behaviour.QueuedAnimationState);
    }
    
    public void PlayAnimation(AnimationState animationState)
    {
        QueuedAnimationState = animationState;
    }

    public void UpdateAnimation()
    {
        _animator.Play(AnimationState.Move.ToString());
    }

    #endregion

}
