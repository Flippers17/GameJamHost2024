using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInputHandler _input;
    [SerializeField] 
    private PlayerHandController _handController;

    [SerializeField]
    private Transform _tableViewPoint;
    [SerializeField]
    private Transform _collegueViewPoint;
    private Transform _targetPoint;

    [SerializeField]
    private float _lerpSpeed = 5f;

    [SerializeField]
    private float _lookSwitchDelay = .5f;
    private float _timeLastSwitched = 0;

    private bool lookingAtTable = true;

    private Transform _cam;

    private void OnEnable()
    {
        _input.OnLookUp += SwitchView;
    }

    // Start is called before the first frame update
    void Start()
    {
        _targetPoint = _tableViewPoint;
        _cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _cam.position = Vector3.Lerp(_cam.position, _targetPoint.position, _lerpSpeed * Time.deltaTime);

        _cam.rotation = Quaternion.Lerp(_cam.rotation, _targetPoint.rotation, _lerpSpeed * Time.deltaTime);
    }

    private void SwitchView()
    {
        if(_timeLastSwitched + _lookSwitchDelay > Time.time)
            return;

        if (lookingAtTable)
        {
            _targetPoint = _collegueViewPoint;
            lookingAtTable = false;
            _handController.DisableHand();
        }
        else
        {
            _targetPoint = _tableViewPoint;
            lookingAtTable = true;
            _handController.EnableHand();
        }

        _timeLastSwitched = Time.time;
    }
}
