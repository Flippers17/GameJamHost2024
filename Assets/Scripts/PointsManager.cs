using UnityEngine;

[CreateAssetMenu(menuName = "PointsManager")]
public class PointsManager : ScriptableObject
{
    public float points;

    public void AddPoints(float amount) => points += amount;
}
