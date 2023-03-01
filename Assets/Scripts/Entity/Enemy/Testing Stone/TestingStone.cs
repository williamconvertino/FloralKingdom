using System;
using Fusion;
using UnityEngine;

public class TestingStone : NetworkBehaviour
{
    public Transform Point1;
    public Transform Point2;
    public float Speed;

    private Rigidbody2D _rb2d;
    private Vector3 _targetPosition;
    private Vector3 _nonTargetPosition;
       
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        transform.position = Point1.position;
        _targetPosition = Point2.position;
        _nonTargetPosition = Point1.position;
    }

    public override void FixedUpdateNetwork()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Runner.DeltaTime);
        _rb2d.MovePosition(newPosition);
        
        if (Vector3.Distance(transform.position, _nonTargetPosition) >= Vector3.Distance(_nonTargetPosition, _targetPosition))
        {
            (_targetPosition, _nonTargetPosition) = (_nonTargetPosition, _targetPosition);
        }
    }
}