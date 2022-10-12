using UnityEngine;

public class FinishService : MonoBehaviour
{
    [SerializeField] private GameScore _gameScore;
    [SerializeField] private FinishView _finishView;
    
    private void Start() =>
        Subscribe();

    private void OnDestroy() =>
        Unsubscribe();

    private void Subscribe() =>
        _gameScore.OnFinish += OnFinishEvent;

    private void Unsubscribe() =>
        _gameScore.OnFinish -= OnFinishEvent;

    private void OnFinishEvent(CharacterTypes winner)
    {
        _finishView.gameObject.SetActive(true);
        _finishView.ShowFinishText(winner);
    }

}