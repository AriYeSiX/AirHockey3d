using UnityEngine;
using UnityEngine.UI;

public class FinishView : MonoBehaviour
{
    [SerializeField] private Text _finishText;

    public void ShowFinishText(CharacterTypes winner)
    {
        switch (winner)
        {
            case CharacterTypes.Enemy:
                SetFinishText("You lose, wanna try again?");
                break;
            case  CharacterTypes.Player:
                SetFinishText("Congratulations! You are winner!");
                break;
        }
    }

    private void SetFinishText(string text) =>
        _finishText.text = text;

}