using UnityEngine;
using UnityEngine.UI;

public class DisableButtonOnClick : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable()
    {
        if(!button)
        {
            if (!TryGetComponent(out button))
            {
                Debug.LogWarning("Couldn't find Button component!", this);
                return;
            }
        }

        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (!button)
            return;

        button.onClick.RemoveAllListeners();

        button.gameObject.SetActive(false);
    }
}
