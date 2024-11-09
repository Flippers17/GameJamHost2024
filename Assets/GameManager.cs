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

    [SerializeField] private Light[] lights;
    [SerializeField] private int loseSceneIndex = 2;

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


        if (_timeUntilTurning < 0 && _timeUntilTurning > -_timeToLookAtPlayer)
        {
            if (PlayerGettingCaught())
            {
                if (CameraShake.s_Initialized)
                {
                    Debug.Log("SHAKE DOES NOT WORK WTF!");
                    CameraShake.TriggerShake(2f, 1f, 0.3f); 
                }
                CatchPlayer(); 
            }
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
        foreach (var light in lights)
        {
            light.color = Color.red;
        }

        _gameOver = true;
        Debug.Log("Player got caught");
        OnPlayerCaught?.Invoke();

        StartCoroutine(Scare());
    }

    private IEnumerator Scare()
    {
        yield return new WaitForSeconds(1);
        SceneLoader.LoadSceneMode(loseSceneIndex);
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
