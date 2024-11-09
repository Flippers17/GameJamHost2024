using TMPro;
using UnityEngine;

public class ScoreHandlerUI : MonoBehaviour
{
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = "Score: " + pointsManager.points;
        pointsManager.ResetScore();
    }
}
