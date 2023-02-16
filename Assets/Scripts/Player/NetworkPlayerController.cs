using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class NetworkPlayerController : MonoBehaviour
{
    
    #region Initialization
    
        private Rigidbody2D _rb2d;
        
        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

    #endregion
    
    #region Movement
    
    [SerializeField] private float speed;

    private Vector2 _direction;
    private void Update()
    {
        _direction = PlayerInput.GetMoveDirection().normalized;
        _rb2d.velocity = _direction * speed;
    }

    #endregion

}
