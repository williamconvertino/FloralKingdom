using UnityEngine;

public class TestingStone : MonoBehaviour
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
        _maxDistance = _direction.magnitude;
        _direction.Normalize();
    }

    private void Move()
    {
        transform.position += _direction * Speed * Time.deltaTime * _flip;
    }
    
    private void Update()
    {
        Move();
        if (Vector3.Magnitude(Point1.position - transform.position) > _maxDistance ||  Vector3.Magnitude(Point2.position - transform.position) > _maxDistance)
        {
            _flip *= -1;
            Move();
        }
    }
}