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
        changed.Behaviour.UpdateDirection();
    }

    public void UpdateDirection()
    {
        _spriteRenderer.flipX = FlipX;
    }

    #endregion
}
