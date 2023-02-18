using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NetworkSpriteRenderer : NetworkBehaviour
{
    #region Initialization

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #endregion

    #region FlipX

    [Networked(OnChanged = nameof(OnFlipXChanged))] public bool FlipX { set; get; }

    public static void OnFlipXChanged(Changed<NetworkSpriteRenderer> changed)
    {
        bool flipXCurrent = changed.Behaviour.FlipX;
        changed.LoadOld();
        bool flipXOld = changed.Behaviour.FlipX;
        
        if (flipXCurrent != flipXOld) changed.Behaviour.OnFlipXRemote(flipXCurrent);
    }

    public void OnFlipXRemote(bool flipX)
    {
        _spriteRenderer.flipX = flipX;
    }

    #endregion
    
}
