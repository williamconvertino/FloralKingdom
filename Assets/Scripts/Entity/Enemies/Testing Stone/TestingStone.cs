using Fusion;
using UnityEngine;

public class TestingStone : NetworkBehaviour
{
    public Transform Point1;
    public Transform Point2;
    public float Speed;

    private Vector3 _direction;
    private float _maxDistance;
    private int _flip = 1;
        
    private void Awake()
    {
        _direction = Point1.position - Point2.position;
        _maxDistance = Vector3.Distance(Point1.position, Point2.position);
        _direction.Normalize();
    }

    private void Move()
    {
        transform.position += _direction * Speed * Runner.DeltaTime * _flip;
    }
    
    public override void FixedUpdateNetwork()
    {
        Move();
        if (Vector3.Distance(Point1.position, transform.position) >= _maxDistance ||  Vector3.Distance(Point2.position, transform.position) >= _maxDistance)
        {
            _flip *= -1;
            Move();
        }
    }
}