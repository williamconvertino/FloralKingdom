
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealthManager : NetworkBehaviour
{

    [Networked(OnChanged = nameof(OnHealthChanged))]
    public int Health { set; get; }
    
    [Header("Health Bar")]
    public TextMeshProUGUI healthText;
    public Vector2 offset = new Vector2(1.0f, 1.0f);
    public Transform entityTransform;
    private bool _showHealthBar;
    private Camera _camera;
    
    #region Initialization
    private void Awake()
    {
        if (healthText != null)
        {
            _showHealthBar = true;
            _camera = Camera.main;
        }
    }
    private void Update()
    {
        if (!_showHealthBar) return;
        healthText.transform.position = _camera.WorldToScreenPoint(entityTransform.position + new Vector3(offset.x, offset.y, 0));
    }
    #endregion

    #region Hit Logic
    public void Hit(int amount)
    {
        Health -= amount;
        if (Health < 0)
        {
            Health = 0;
            OnDeath();
        }
    }
    
    public void OnDeath()
    {
        Debug.Log("Entity died");
    }

    public void UpdateHealthBar()
    {
        if (!_showHealthBar) return;
        healthText.text = "Health: " + Health;
    }

    public static void OnHealthChanged(Changed<EntityHealthManager> changed)
    {
        changed.Behaviour.UpdateHealthBar();
    }
    
    #endregion
}
