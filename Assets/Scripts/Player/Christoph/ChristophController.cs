using UnityEngine;
public class ChristophController : PlayerController
{
    
    #region Action

    protected override void UpdateAction()
    {
        if (playerState == PlayerState.Action_Locked) return;
        
        if (DoAttack)
        {
            playerState = PlayerState.Action_Locked;
            entityAnimator.PlayAnimation(EntityAnimationState.Headbutt);
            entityActionManager.StartAction("Headbutt");
        }
    }

    #endregion

}
