using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningSignBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Color _defaultColor;
    [SerializeField]
    private Color _mediumWarningColor;
    [SerializeField]
    private Color _extremeWarningColor;
    [SerializeField]
    private Color _lookingColor;

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
        Color newColor  = _defaultColor;
        switch (stage)
        {
            case 0: 
                newColor = _defaultColor;
                break;

            case 1:
                newColor = _mediumWarningColor;
                break;

            case 2:
                newColor = _extremeWarningColor;
                break;
            
            case 3:
                newColor = _lookingColor;
                break;

            default:
                newColor = _defaultColor;
                break;
        }

        _image.color = newColor;
    }


    private void DisableSign()
    {
        _image.enabled = false;
    }

    private void EnableSign()
    {
        _image.enabled = true;
    }
}
