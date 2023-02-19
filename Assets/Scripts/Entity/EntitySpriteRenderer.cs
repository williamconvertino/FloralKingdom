using Fusion;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EntitySpriteRenderer : NetworkBehaviour
{
    #region Initialization

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #endregion

    #region FlipX

    [Networked(OnChanged = nameof(OnFlipXChanged))] [HideInInspector] public NetworkBool FlipX { set; get; }

    public static void OnFlipXChanged(Changed<EntitySpriteRenderer> changed)
    {
        bool flipXCurrent = changed.Behaviour.FlipX;
        changed.LoadOld();
        bool flipXOld = changed.Behaviour.FlipX;
        
        if (flipXCurrent != flipXOld) changed.Behaviour.OnFlipX(flipXCurrent);
    }

    public void OnFlipX(bool flipX)
    {
        _spriteRenderer.flipX = flipX;
    }

    #endregion
}
