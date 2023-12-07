using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MoveablePlatformBehavior : MonoBehaviour
{
    [SerializeField]
    PlatformMovementTypes _types;

    [SerializeField]
    float _horizontalSpeed = 5;
    [SerializeField]
    float _verticalSpeed = 3;
    [SerializeField]
    float _horizontalDistance = 5;
    [SerializeField]
    float _verticalDistance = 3;

    [SerializeField]
    List<Transform> _pathTransforms = new List<Transform>();

    List<Vector2> _destinations = new List<Vector2>();

    Vector2 _startPosition;
    Vector2 _endPosition;

    int _destinationIndex = 0;
    float _timer;
    [SerializeField]
    [Range(0, 0.1f)]
    float _customMovementTimeChangeFactor = 0.05f;
    // Start is called before the first frame update
    void Start()
    {

        foreach(Transform t in _pathTransforms)
        {
            _destinations.Add(t.position);
        }

        _destinations.Add(transform.position);

        _startPosition = transform.position;
        _endPosition = _destinations[_destinationIndex];

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (_types == PlatformMovementTypes.CUSTOM)
        {
            if (_timer >= 1) // This mean i reached to my destination.
            {
                _timer = 0;
                _destinationIndex++;

                if (_destinationIndex >= _destinations.Count)
                {
                    _destinationIndex = 0;
                }

                _startPosition = transform.position;
                _endPosition = _destinations[_destinationIndex];

            }

            else if(_timer < 1)
            {
                _timer += _customMovementTimeChangeFactor;
            } 


        }

    }


    void Move()
    {
        switch(_types)
        {
            case PlatformMovementTypes.HORIZONTAL:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPosition.x,
                                         transform.position.y);
                break;
            case PlatformMovementTypes.VERTICAL:
                transform.position = new Vector2(transform.position.x,
                                                 Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPosition.y);
                break;
            case PlatformMovementTypes.DIAGONAL_RIGHT:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPosition.x,
                                                  Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPosition.y);
                break;
            case PlatformMovementTypes.DIAGONAL_LEFT:
                transform.position = new Vector2(_startPosition.x - Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) ,
                                                 Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPosition.y);
                break;
            case PlatformMovementTypes.CUSTOM:
                transform.position = Vector2.Lerp(_startPosition,_endPosition,_timer );
                break;
        }
    }
}
