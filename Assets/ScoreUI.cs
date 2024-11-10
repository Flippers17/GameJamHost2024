using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private PointsManager pointsManager;

    private void OnEnable()
    {
        if (!pointsManager)
        { 
            Debug.LogWarning("No PointsManager on object!", this); 
            return;
        }

        scoreText.text = "Score: " + pointsManager.points;

        pointsManager.onScoreAdded += UpdateText;
    }

    private void OnDisable()
    {
        pointsManager.onScoreAdded -= UpdateText;
    }

    private void UpdateText(int score)
    {
        scoreText.text = "Score: " + score;
    }

}
