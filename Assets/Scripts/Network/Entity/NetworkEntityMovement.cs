public class NetworkEntityMovement : EntityMovement
{
    #region Initialization

    private NetworkSpriteRenderer _networkSpriteRenderer;

    private void Awake()
    {
        _networkSpriteRenderer = GetComponentInChildren<NetworkSpriteRenderer>();
    }

    #endregion
    protected override void UpdateSpriteDirection()
    {
        if (Direction.x > 0) FlipX = false;
        if (Direction.x < 0) FlipX = true;
        if (FlipX != _networkSpriteRenderer.FlipX) _networkSpriteRenderer.FlipX = FlipX;
    }
}