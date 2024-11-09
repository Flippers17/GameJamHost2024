using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;

    [SerializeField]
    private float _minDistractedDuration = 10f;
    [SerializeField]
    private float _maxDistractedDuration = 20f;
    private float _currentDistractedDuration = 1f;

    [SerializeField]
    private float _minTimeLookingAtPlayer = 2f;
    [SerializeField]
    private float _maxTimeLookingAtPlayer = 2f;
    private float _timeToLookAtPlayer = 0;

    [SerializeField]
    private ColleagueBehaviour _colleague;

    public UnityEvent OnPlayerCaught;

    public float _timeUntilTurning = 100f;

    [SerializeField]
    private WarningSignPort _warningSignPort;

    private bool _gameOver = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _timeUntilTurning = Random.Range(_minDistractedDuration, _maxDistractedDuration);
        _currentDistractedDuration = _timeUntilTurning;
        _timeToLookAtPlayer = Random.Range(_minTimeLookingAtPlayer, _maxTimeLookingAtPlayer);
    }


    // Update is called once per frame
    void Update()
    {

        if (_gameOver)
            return;

        _timeUntilTurning -= Time.deltaTime;

        ChangeWarningState(_timeUntilTurning/_currentDistractedDuration);

        if (_timeUntilTurning < 1)
            _colleague.LookTowardsPlayer();

        if (_timeUntilTurning < 0 && _timeUntilTurning > -_timeToLookAtPlayer)
        {
            if (PlayerGettingCaught())
                CatchPlayer();
        }
        else if (_timeUntilTurning < -_timeToLookAtPlayer)
        {
            ResetTimer();
        }
    }


    private bool PlayerGettingCaught()
    {
        return MiniGameHandler.Instance.InMiniGame;
    }


    private void CatchPlayer()
    {
        _gameOver = true;
        Debug.Log("Player got caught");
        OnPlayerCaught?.Invoke();
    }

    private void ChangeWarningState(float currentTimeProgress)
    {
        if (_timeUntilTurning <= 0)
        {
            _warningSignPort.ChangeStage(3);

        }
        else if (currentTimeProgress < .3f)
        {
            _warningSignPort.ChangeStage(2);
        }
        else if (currentTimeProgress < .5f)
        {
            _warningSignPort.ChangeStage(1);
        }
        else
        {
            _warningSignPort.ChangeStage(0);
        }
    }

    private void ResetTimer()
    {
        _timeUntilTurning = Random.Range(_minDistractedDuration, _maxDistractedDuration);
        _currentDistractedDuration = _timeUntilTurning;
        _timeToLookAtPlayer = Random.Range(_minTimeLookingAtPlayer, _maxTimeLookingAtPlayer);

    }
}
