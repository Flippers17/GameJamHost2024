using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private GameObject aboutHUD;

    public void OnButtonPress()
    {
        Time.timeScale = 1.0f;

        aboutHUD.SetActive(false);
    }
}
