public enum EntityAnimationState
{
    //General
    None,
    Idle,
    Move,
    
    //Christoph
    Headbutt
    
}

static class EntityAnimationStateExtensions
{
    public static bool IsAction(this EntityAnimationState animationState)
    {
        return !(animationState == EntityAnimationState.None || animationState == EntityAnimationState.Idle ||
                 animationState == EntityAnimationState.Move);
    }  
}
