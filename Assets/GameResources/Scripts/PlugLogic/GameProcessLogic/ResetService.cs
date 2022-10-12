using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetService : MonoBehaviour
{
    private List<Vector3> _startPositions = new List<Vector3>();

    [SerializeField] private List<Rigidbody> _objectsRb;
    
    [SerializeField] private List<Transform> _objectPositions = new List<Transform>();

    [SerializeField] private  GameScore _gameScore;

    private void Start()
    {
        Init();
        Subscribe();
    }

    private void OnDestroy() =>
        Unsubscribe();

    private void Init()
    {
        foreach (Transform obj in _objectPositions)
            _startPositions.Add(obj.transform.position);
    }

    private void Subscribe() => _gameScore.OnFinish += OnFinishEvent;
    
    private void Unsubscribe() => _gameScore.OnFinish -= OnFinishEvent;

    private void OnFinishEvent(CharacterTypes plug)
    {
        foreach (var rb in _objectsRb)
            rb.isKinematic = true;
    }
    
    /// <summary>
    /// Reset all object positions to defaults 
    /// </summary>
    public void ResetObjectPositions()
    {
        for (int i = 0; i < _objectPositions.Count; i++)
        {
            _objectsRb[i].isKinematic = true;
            _objectPositions[i].transform.position = _startPositions[i];
            _objectsRb[i].isKinematic = false;
        }
    }
}
