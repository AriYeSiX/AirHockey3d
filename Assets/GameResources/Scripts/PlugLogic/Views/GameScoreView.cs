using UnityEngine;
using UnityEngine.UI;

public class GameScoreView : MonoBehaviour
{
    [SerializeField] private Text _playerScoreText;
    [SerializeField] private Text _enemyScoreText;
    [SerializeField] private GameScore _gameScore;
    
    private void Start() =>
        Subscribe();

    private void OnDestroy() =>
        Unsubscribe();

    private void Subscribe() =>
        _gameScore.OnScoreUpdate += SetScores;

    private void Unsubscribe() =>
        _gameScore.OnScoreUpdate -= SetScores;

    private void SetScores()
    {
        SetScoreToText(_playerScoreText, _gameScore.PlayerScore);
        SetScoreToText(_enemyScoreText, _gameScore.EnemyScore);
    }

    private void SetScoreToText(Text text, int score) =>
        text.text = score.ToString();
    
}