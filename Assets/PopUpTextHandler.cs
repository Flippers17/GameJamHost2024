using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpTextHandler : MonoBehaviour
{
    public static PopUpTextHandler Instance;

    [SerializeField]
    private TextMeshProUGUI _text;
    float _timeToShwoText = 0;
    

    private void Awake()
    {
        Instance = this;
    }

    
    public void SetText(string text, float timeShown)
    {
        _text.text = text;
        _timeToShwoText = timeShown;
    }

    public void Update()
    {
        if (_timeToShwoText <= 0)
            _text.text = "";

        _timeToShwoText -= Time.deltaTime;
    }
}
