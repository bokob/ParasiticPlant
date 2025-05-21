using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] bool _isHorizontal;
    [SerializeField] bool _isVertical;
    [SerializeField] float _speed;
    [SerializeField] float _distance;
    Vector2 _startPoint;
    Vector2 _endPoint;

    void Start()
    {
        _startPoint = transform.position;

        if (_isHorizontal)
        {
            _endPoint = new Vector2(_startPoint.x + _distance, _startPoint.y);
        }
        else if (_isVertical)
        {
            _endPoint = new Vector2(_startPoint.x, _startPoint.y + _distance);
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(_startPoint, _endPoint, Mathf.PingPong(Time.time * _speed, 1));
    }
}