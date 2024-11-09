using UnityEngine;
using UnityEngine.UI;

public class CutVeinsMiniGame : MiniGame
{
    [Header("Objects")]
    [SerializeField] private Image progressBar;
    [SerializeField] private Button[] veinButtons;

    private int buttonsClicked = 0;

    public override void OnStart(PlayerHandController controller)
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
            onCompletedSuccesfully?.Invoke();
        }
    }
}
