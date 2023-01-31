using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    #region Update
    
    private void Update()
    {
        UpdateAnimation();
    }

    #endregion

    #region AnimationState

    public enum AnimationState
    {
        Idle,
        Move,
        Attack
    }

    #endregion

    #region PlayAnimation

    private AnimationState _currentAnimationState;

    public void PlayAnimation(AnimationState animationState)
    {
        if (animationState == _currentAnimationState) return;

        _currentAnimationState = animationState;
        
        LoadAnimation(animationState.ToString());
        
    }
    
    //I didn't want to add this, but my animations kept getting stuck
    //when I attacked after the "OnAttackComplete" method triggered.
    public void OnAnimationComplete()
    {
        _isAttackInProgress = false;
        LoadAnimation(AnimationState.Idle.ToString());
    }
    
    #endregion

    #region PlayAttackAnimation

    private bool _isAttackInProgress = false;

    public void PlayAttackAnimation(string animationName)
    {
        if (_isAttackInProgress) return;
        
        _isAttackInProgress = true;
       
        _currentAnimationState = AnimationState.Attack;
        LoadAnimation(animationName);
    }
    
    public void OnAttackAnimationComplete()
    {
        _isAttackInProgress = false;
    }

    public bool IsAttackInProgress()
    {
        return _isAttackInProgress;
    }

    #endregion

    #region Load Animation

    private string _animationToPlay;
    private bool _isAnimationQueued = false;

    //This in-between method is necessary to avoid synchronization issues
    //animator.play must be called during update step or else animations
    //can be skipped when played in quick succession
    private void LoadAnimation(String animationName)
    {
        _animationToPlay = animationName;
        _isAnimationQueued = true;
        print("Queued: " + animationName);
    }

    private void UpdateAnimation()
    {
        if (_isAnimationQueued)
        {
            _animator.Play(_animationToPlay);
            
            print("Playing: " + _animationToPlay);
        }

        _isAnimationQueued = false;
    }

    #endregion
}
