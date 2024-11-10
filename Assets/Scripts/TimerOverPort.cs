using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "TimeOverPort")]
public class TimerOverPort : ScriptableObject
{
    public UnityAction onTimeOver;

    public void InvokePort() => onTimeOver?.Invoke();
}
