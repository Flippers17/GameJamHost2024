using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static float shakeDuration;
    private static float shakeMagnitude = .7f; //0-.4
    private static float dampingSpeed = .4f; //0-.9

    private Vector3 initial3DCamPosition;

    public static bool s_Initialized = false;

    private void Awake()
    {
        initial3DCamPosition = transform.localPosition;
        s_Initialized = true;
    }

    private void OnDisable()
    {
        s_Initialized = false;
    }

    public static void TriggerShake(float duration, float _shakeMagnitude, float _dampingSpeed)
    {
        shakeDuration = duration;

        shakeMagnitude = _shakeMagnitude;

        dampingSpeed = _dampingSpeed;
    }

    private void Update()
    {
        if(shakeDuration > 0)
        {
            Vector3 ranInsideUnitCircle = (Vector3)Random.insideUnitCircle * shakeMagnitude;
            Vector3 shakePos = Vector3.Lerp(transform.localPosition, initial3DCamPosition + ranInsideUnitCircle, dampingSpeed);

            transform.localPosition = shakePos;
            
            shakeDuration -= Time.deltaTime;
            
            if (shakeDuration <= 0)
            {
                shakeDuration = 0;
                transform.localPosition = Vector3.Lerp(transform.localPosition, initial3DCamPosition, 1f);
            }
        }
    }
}
