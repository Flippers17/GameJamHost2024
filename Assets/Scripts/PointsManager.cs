using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "PointsManager")]
public class PointsManager : ScriptableObject
{
    public UnityAction<int> onScoreAdded;

    public float points;

    public void AddPoints(float amount)
    {
        points += amount;
        onScoreAdded?.Invoke((int)points);
    }

    public void ResetScore()  => points = 0;
}
