public class ChristophController : PlayerController
{

    protected override void UpdatePlayerStateStart()
    {
        if (! (playerState == PlayerState.Action_Unlocked || playerState == PlayerState.Action_Locked))
        {
            DoDirectionChange = true;
            DoMovement = true;
        }
    }
    
    #region Action

    protected override void UpdateAction()
    {
        if (playerState == PlayerState.Action_Locked) return;
        
        if (DoAttack)
        {
            playerState = PlayerState.Action_Locked;
            DoMovement = false;
            DoDirectionChange = false;
            entityAnimator.PlayAnimation(EntityAnimationState.Headbutt);
            entityActionManager.StartAction("Headbutt");
        }
    }

    #endregion

}
