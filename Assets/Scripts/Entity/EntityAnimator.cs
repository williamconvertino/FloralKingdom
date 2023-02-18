using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimationEventDispatcher))]
public class EntityAnimator : NetworkBehaviour
{
    
    #region Initialization and Update
    private Animator _animator;
    private void Start ()
    {
        _animator = GetComponent<Animator>();
        AnimationEventDispatcher eventDispatcher = GetComponent<AnimationEventDispatcher>();
        eventDispatcher.OnAnimationComplete.AddListener(e => EndAnimation());
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
    public static void OnNewAnimationState(Changed<EntityAnimator> changed)
    {
        changed.Behaviour.RunAnimator(changed.Behaviour.NewAnimationState.ToString());
    }
    private void LoadAnimationState(EntityAnimationState animationState, bool doAnimationLock = true)
    {
        AnimationInProgress = true;
        AnimationLocked = doAnimationLock;
        
        NewAnimationState = animationState;
    }
    public void RunAnimator(String animationName)
    {
        _animator.Play(animationName);
    }

    #endregion
    
    #region Animation Locks

    public bool AnimationLocked { set; get; }
    public bool AnimationInProgress { set; get; }
    
    public void RemoveAnimationLock()
    {
        AnimationLocked = false;
    }
    
    public void EndAnimation()
    {
        AnimationInProgress = false;
        RemoveAnimationLock();
    }

    private static HashSet<EntityAnimationState> _passiveStates = new HashSet<EntityAnimationState>()
        {
            EntityAnimationState.Idle,
            EntityAnimationState.Move
        };

    #endregion

    #region Update Animation
    
    public void UpdateAnimation()
    {
        EntityAnimationState nextAnimationState = _queuedAnimationState;
        _queuedAnimationState = EntityAnimationState.None;

        if (AnimationLocked) return;

        bool passiveAnimation = _passiveStates.Contains(nextAnimationState);

        if (passiveAnimation && AnimationInProgress) return;
        
        LoadAnimationState(nextAnimationState, !passiveAnimation);
    }

    
    
    #endregion

}
