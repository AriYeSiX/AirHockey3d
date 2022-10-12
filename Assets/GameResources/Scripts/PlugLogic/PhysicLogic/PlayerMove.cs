using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private const float CONST_HEIGHT = -5.6f;
    
    private AndroidActions _androidActions;
    private Vector2 inputVector = Vector2.zero;

    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private float _characterSpeed;
    
    private void Start()
    {
        _androidActions = new AndroidActions();
        
        _androidActions.Enable();
    }

    private void FixedUpdate()
    {
        inputVector =  _androidActions.AndroidMap.LeftStick.ReadValue<Vector2>();
        _playerRigidbody.velocity = 
            new Vector3(inputVector.x * _characterSpeed, CONST_HEIGHT, inputVector.y * _characterSpeed);
    }
}
