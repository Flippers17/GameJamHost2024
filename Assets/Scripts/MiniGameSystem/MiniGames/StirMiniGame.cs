using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class StirMiniGame : MiniGame
{
    [SerializeField]
    private InputAction quickTimeValue;
    private Vector2 targetPress = new Vector2(1, 0);
    [SerializeField]
    private float handMoveMultiplier = 5f;

    [SerializeField]
    List<StirButton> buttons = new List<StirButton>();
    private int currentIndex = -1;

    [SerializeField]
    private float _delayBetweenInputs = .3f;
    private float _timeLastPressed = 0;

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        base.OnStart(controller, organ);
        quickTimeValue.performed += OnInput;
        quickTimeValue.Enable();
        GoToNextTargetPress();
    }

    private void OnDisable()
    {
        quickTimeValue.performed -= OnInput;
    }


    private void OnInput(InputAction.CallbackContext context)
    {
        if(Time.time < _timeLastPressed + _delayBetweenInputs)
            return;

        if (context.ReadValue<Vector2>().x * targetPress.x <= 0 && context.ReadValue<Vector2>().y * targetPress.y <= 0)
            return;

        playerHand.MoveHand(targetPress * handMoveMultiplier * (progress / neededProgress));
        organ.MoveOrgan(targetPress * handMoveMultiplier * (progress / neededProgress));
        IncreaseProgress(1);
        _timeLastPressed = Time.time;
        GoToNextTargetPress();
    }


    private void GoToNextTargetPress()
    {
        currentIndex++;
        currentIndex %= buttons.Count;
        targetPress = buttons[currentIndex].position;

        for(int i = 0; i < buttons.Count; i++)
        {
            if(i == currentIndex)
                buttons[i].buttonObject.SetActive(true);
            else
                buttons[i].buttonObject.SetActive(false);
        }
    }
}


[System.Serializable]
public class StirButton
{
    public GameObject buttonObject;
    public Vector2 position;
}
