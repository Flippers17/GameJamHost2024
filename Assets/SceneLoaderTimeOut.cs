using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderTimeOut : MonoBehaviour
{
    [SerializeField] private TimerOverPort port;

    private void OnEnable()
    {
        port.onTimeOver += LoadScene;
    }

    private void OnDisable()
    {
        port.onTimeOver -= LoadScene;
    }

    private void LoadScene()
    {
        SceneLoader.LoadSceneMode(3);
    }
}
