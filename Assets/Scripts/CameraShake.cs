using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShake : MonoBehaviour
{
    private static float shakeDuration;
    private static float shakeMagnitude = .7f; //0-.4
    private static float dampingSpeed = .4f; //0-.9

    private Vector3 initial3DCamPosition;

    public static bool s_Initialized = false;


    private void Awake()
    {
        initial3DCamPosition = transform.position;
        s_Initialized = true;
    }

    private void OnDisable()
    {
        s_Initialized = false;
    }

    public static void TriggerShake(float duration, float _shakeMagnitude, float _dampingSpeed)
    {
        if(!s_Initialized)
            return;

        if (duration < shakeDuration)
            return;

        shakeDuration = duration;

        shakeMagnitude = _shakeMagnitude;

        dampingSpeed = _dampingSpeed;
    }

    private void Update()
    {
        if(shakeDuration > 0)
        {
            Vector3 ranInsideUnitCircle = (Vector3)Random.insideUnitCircle * shakeMagnitude;
            Vector3 shakePos = Vector3.Lerp(transform.position, initial3DCamPosition + ranInsideUnitCircle, dampingSpeed);

            transform.position = shakePos;
            
            shakeDuration -= Time.deltaTime;
            
            if (shakeDuration <= 0)
            {
                shakeDuration = 0;
                transform.position = initial3DCamPosition;
            }
        }
    }
}
