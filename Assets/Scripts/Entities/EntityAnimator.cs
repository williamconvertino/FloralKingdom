using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EntityAnimator : MonoBehaviour
{
    private Animator _animator;

    #region Init & Update

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
    
    private AnimationState _currentAnimationState;
    private AnimationState _queuedAnimation;
    private string _queuedAttackName;
    
    public void PlayAnimation(AnimationState animationState)
    {
        _queuedAnimation = animationState;
    }
    
    public void PlayAttackAnimation(string attackName)
    {
        _queuedAnimation = AnimationState.Attack;
        _queuedAttackName = attackName;
    }
    
    //This system is bad, but having this logic in PlayAnimation causes synchronization issues.
    private void UpdateAnimation()
    {
        if (_isAttacking) return;

        if (_queuedAnimation == AnimationState.Attack)
        {
            _isAttacking = true;
            _currentAnimationState = AnimationState.Attack;
            _animator.Play(_queuedAttackName);
            return;
        }

        if (_currentAnimationState == _queuedAnimation) return;

        _currentAnimationState = _queuedAnimation;
        _animator.Play(_currentAnimationState.ToString());
    }
    public void OnAnimationEnd()
    {
        _queuedAnimation = AnimationState.Idle;
    }
    
    #endregion

    #region Attack State
    private bool _isAttacking;
    public void OnAttackAnimationEnd()
        {
            _isAttacking = false;
        }
        
        public bool IsAttacking()
        {
            return _isAttacking;
        }

    #endregion
    
}
