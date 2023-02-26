
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealthManager : NetworkBehaviour
{

    [Networked(OnChanged = nameof(OnHealthChanged))]
    public int Health { set; get; }
    
    [Header("Health Bar")]
    public TextMeshPro healthText;
    private bool _showHealthBar;
    private Camera _camera;
    
    #region Initialization
    private void Awake()
    {
        if (healthText != null)
        {
            _showHealthBar = true;
        }
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
        if (_showHealthBar) healthText.text = "Health: " + Health;
    }

    public static void OnHealthChanged(Changed<EntityHealthManager> changed)
    {
        changed.Behaviour.UpdateHealthBar();
    }
    
    #endregion
}
