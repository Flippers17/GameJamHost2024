using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class MiniGame : MonoBehaviour
{
    public UnityAction onWin;
    public UnityAction onLost;

    [SerializeField]
    protected float neededProgress = 100;
    protected float progress = 0;
    [SerializeField]
    private Image _progressFill;

    [SerializeField]
    private string _animationState;

    protected PlayerHandController playerHand;
    protected Organ organ;

    public virtual void OnStart(PlayerHandController controller, Organ organ)
    {
        playerHand = controller;
        this.organ = organ;
        playerHand.PlayMinigameAnimation(_animationState);
    }

    protected virtual void Win()
    {
        organ.GetComponent<Rigidbody>().isKinematic = false;
        playerHand.EnableHand();
        organ.SetFinished();
        organ.canBePickedUp = true;
        playerHand.PickUpItem(organ);
        onWin?.Invoke();
        Destroy(gameObject);
        playerHand.WinMinigameAnim();
    }

    protected virtual void Lose()
    {
        //organ.GetComponent<Rigidbody>().isKinematic = false;
        playerHand.EnableHand();
        playerHand.LoseMiniagemAnim();

        if (CameraShake.s_Initialized)
            CameraShake.TriggerShake(0.5f, 0.2f, 0.1f);

        onLost?.Invoke();
        Destroy(gameObject);
    }
    

    public virtual void IncreaseProgress(float amount)
    {
        progress += amount;

        _progressFill.fillAmount = progress/neededProgress;
        if(progress > neededProgress)
            Win();
    }


    public void Quit()
    {
        Lose();
    }
}
