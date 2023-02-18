
using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(EntityAnimator))]
public class NetworkEntityAnimator : NetworkBehaviour
{
   #region Initialization

   private EntityAnimator _entityAnimator;

   private void Awake()
   {
      _entityAnimator = GetComponent<EntityAnimator>();
   }

   #endregion
   
   #region Animation State

   [Networked(OnChanged = nameof(OnAnimationStateChange))] public EntityAnimator.AnimationState AnimationState { set; get; }

   public static void OnAnimationStateChange(Changed<NetworkEntityAnimator> changed)
   {
      changed.Behaviour.PlayAnimation(changed.Behaviour.AnimationState);
   }

   public void PlayAnimation(EntityAnimator.AnimationState animationState)
   {
      _entityAnimator.PlayAnimation(animationState);
   }
   
   #endregion
  
}