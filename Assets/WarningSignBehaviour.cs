using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningSignBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField] private TMP_Text warningText;

    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite mediumWarningSprite;
    [SerializeField] private Sprite extremeWarningSprite;
    [SerializeField] private Sprite lookingSprite;

    [SerializeField]
    private WarningSignPort port;

    private void OnEnable()
    {
        port.OnStageUpdate += SetWarningColor;
        port.OnDisableSign += DisableSign;
        port.OnEnableSign += EnableSign;
    }

    private void OnDisable()
    {
        port.OnStageUpdate -= SetWarningColor;
    }

    private void SetWarningColor(int stage)
    {
        Sprite newSrite = defaultSprite;
        switch (stage)
        {
            case 0:
                newSrite = defaultSprite;
                break;

            case 1:
                newSrite = mediumWarningSprite;
                break;

            case 2:
                newSrite = extremeWarningSprite;
                break;
            
            case 3:
                newSrite = extremeWarningSprite;
                break;

            default:
                newSrite = defaultSprite;
                break;
        }


        _image.sprite = newSrite;
    }


    private void DisableSign()
    {
        if (!_image) return;

        _image.enabled = false;
        warningText.enabled = false;
    }

    private void EnableSign()
    {
        if (!_image) return;

        _image.enabled = true;
        warningText.enabled = true;
    }
}
