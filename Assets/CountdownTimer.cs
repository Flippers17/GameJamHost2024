using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float secounds = 360;

    [SerializeField] private TimerOverPort timeOverPort;
    [SerializeField] private TMP_Text timerText;

    private void Update()
    {
        if (secounds <= 0)
            return;

        secounds -= Time.deltaTime;

        timerText.text = $"Time: {Mathf.FloorToInt(secounds / 60)}:{Mathf.FloorToInt(secounds % 60)}";

        if(secounds < 0)
        {
            timerText.text = "Time: 0:00";
            timeOverPort.InvokePort();
        }
    }
}
