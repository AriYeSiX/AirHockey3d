using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    private const string NOT_INTERACTABLE = "not interactable";
    
    [SerializeField] private Rigidbody puckRb;
    [SerializeField] private float force;

    public event Action OnPlayerGateEnter;
    public event Action OnEnemyGateEnter;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(NOT_INTERACTABLE))
            return;

        Vector3 position = other.transform.position;
        Vector3 direction = puckRb.transform.position - position;
        puckRb.AddForceAtPosition(direction.normalized * force, position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            OnEnemyGateEnter?.Invoke();

        if (other.CompareTag("Player"))
            OnPlayerGateEnter?.Invoke();
    }
}
