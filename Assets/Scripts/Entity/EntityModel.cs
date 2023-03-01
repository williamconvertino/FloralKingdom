using Fusion;
using UnityEngine;

public class EntityModel : NetworkBehaviour
{
    
    #region FlipX
    [Networked(OnChanged = nameof(OnFlipXChanged))] [HideInInspector] public NetworkBool FlipX { set; get; }

    public static void OnFlipXChanged(Changed<EntityModel> changed)
    {
        changed.Behaviour.UpdateDirection();
    }

    public void UpdateDirection()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (FlipX ? -1 : 1), transform.localScale.y, transform.localScale.z);
    }

    #endregion
}
