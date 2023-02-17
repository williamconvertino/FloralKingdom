using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
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

    public Vector2 Direction { set; get; }
    private void Update()
    {
        _rb2d.velocity = Direction.normalized * speed;
    }

    #endregion
}
