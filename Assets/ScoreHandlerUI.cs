using TMPro;
using UnityEngine;

public class ScoreHandlerUI : MonoBehaviour
{
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        scoreText.text = "Score: " + pointsManager.points;
        pointsManager.ResetScore();
        highScoreText.text = "High Score: " + pointsManager.highScore;
    }
}
