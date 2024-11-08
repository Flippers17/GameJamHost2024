using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutVeinsMiniGame : MiniGameBase
{
    [Header("Objects")]
    [SerializeField] private Image progressBar;
    [SerializeField] private Button[] veinButtons;

    private int buttonsClicked = 0;

    private void Start()
    {
        OnStart();
    }

    public override void OnStart()
    {
        foreach (var button in veinButtons)
            button.onClick.AddListener(OnVeinButtonClicked);
    }

    private void OnDisable()
    {
        foreach (var button in veinButtons)
            button.onClick.RemoveAllListeners();
    }

    private void OnVeinButtonClicked()
    {
        buttonsClicked++;

        progressBar.fillAmount = (float)buttonsClicked / veinButtons.Length;

        if(buttonsClicked == veinButtons.Length)
        {
            Debug.Log("Finished!");
        }
    }
}
