using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float CONST_HEIGHT = -5.6f;
    
    [SerializeField] private Transform _puck;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _enemy;
    [SerializeField] private Rigidbody _enemyRb;
    
    
    private Vector3 _puckPosition;
    private Vector3 _startPosition;

    private void Start() =>
        _startPosition = _enemy.position;

    private void FixedUpdate()
    {
        _puckPosition = _puck.position;

        if (_puckPosition.z < 15f && _puckPosition.z >= 8.1f)
        {
            Vector3 direction = _puck.position - _enemy.position;
            _enemyRb.velocity = new Vector3(direction.x * _speed * Time.deltaTime, CONST_HEIGHT, direction.z * _speed * Time.deltaTime);
        }
        else
        {
            Vector3 direction = _startPosition - _enemy.position;
            _enemyRb.velocity = new Vector3(direction.x * GetMoveCoefficient(), CONST_HEIGHT, direction.z * GetMoveCoefficient());
        }
    }

    private float GetMoveCoefficient() =>
        _speed * Time.deltaTime;
}
