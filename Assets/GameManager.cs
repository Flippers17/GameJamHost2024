using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public GameManager instance;

    [SerializeField]
    private float _minDistractedDuration = 10f;
    [SerializeField]
    private float _maxDistractedDuration = 20f;

    [SerializeField]
    private float _minTimeLookingAtPlayer = 2f;
    [SerializeField]
    private float _maxTimeLookingAtPlayer = 2f;
    private float _timeToLookAtPlayer = 0;

    [SerializeField]
    private ColleagueBehaviour _colleague;

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
        _timeToLookAtPlayer = Random.Range(_minTimeLookingAtPlayer, _maxTimeLookingAtPlayer);
    }


    // Update is called once per frame
    void Update()
    {

        if (_gameOver)
            return;

        _timeUntilTurning -= Time.deltaTime;

        ChangeWarningState();

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
    }

    private void ChangeWarningState()
    {
        if (_timeUntilTurning <= 0)
        {
            _warningSignPort.ChangeStage(3);

        }
        else if (_timeUntilTurning < 5f)
        {
            _warningSignPort.ChangeStage(2);
        }
        else if (_timeUntilTurning < 10f)
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
        _timeToLookAtPlayer = Random.Range(_minTimeLookingAtPlayer, _maxTimeLookingAtPlayer);

    }
}
