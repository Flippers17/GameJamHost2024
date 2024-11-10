using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private PointsManager pointsManager;

    private void OnEnable()
    {
        highScoreText.text = "Current High Score: " + pointsManager.highScore;
    }
}
