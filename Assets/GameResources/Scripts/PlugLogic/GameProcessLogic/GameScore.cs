using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameScore : MonoBehaviour
{
    private const int FINISH_SCORE = 10;
    
    private int _playerScore;
    /// <summary>
    /// Current player score 
    /// </summary>
    public int PlayerScore => _playerScore;

    private int _enemyScore;
    /// <summary>
    /// Current enemy score
    /// </summary>
    public int EnemyScore => _enemyScore;

    private CharacterTypes _characterType;
    
    [SerializeField] private Puck _puck;
    [SerializeField] private ResetService _resetService;

    /// <summary>
    /// Player or enemy score was updaited
    /// </summary>
    public event Action OnScoreUpdate;
    
    /// <summary>
    /// Return winner
    /// </summary>
    public event Action<CharacterTypes> OnFinish;
    
    private void Start()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _puck.OnEnemyGateEnter += OnEnemyGateEnterEvent;
        _puck.OnPlayerGateEnter += OnPlayerGateEnterEvent;
    }

    private void Unsubscribe()
    {
        _puck.OnEnemyGateEnter -= OnEnemyGateEnterEvent;
        _puck.OnPlayerGateEnter -= OnPlayerGateEnterEvent;
    }

    private void OnEnemyGateEnterEvent()
    {
        _playerScore++;
        OnRoundEnd();
    }
    
    private void OnPlayerGateEnterEvent()
    {
        _enemyScore++;
        OnRoundEnd();
    }

    private void CheckFinish()
    {
        if (_playerScore >= FINISH_SCORE || _enemyScore >= FINISH_SCORE)
        {
            _characterType = _playerScore >= FINISH_SCORE ? CharacterTypes.Player : CharacterTypes.Enemy;
            OnFinish?.Invoke(_characterType);
        }
    }

    private void OnRoundEnd()
    {
        _resetService.ResetObjectPositions();
        OnScoreUpdate?.Invoke();
        CheckFinish();
    }

    /// <summary>
    /// Reset player and enemy scores
    /// </summary>    
    public void ResetScores()
    {
        _enemyScore = 0;
        _playerScore = _enemyScore;
        OnScoreUpdate?.Invoke();
    }
}