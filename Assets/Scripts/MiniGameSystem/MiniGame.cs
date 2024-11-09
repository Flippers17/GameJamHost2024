using UnityEngine;
using UnityEngine.Events;

public abstract class MiniGame : MonoBehaviour
{
    public UnityAction onWin;
    public UnityAction onLost;

    protected PlayerHandController playerHand;
    protected Organ organ;

    public virtual void OnStart(PlayerHandController controller, Organ organ)
    {
        playerHand = controller;
        this.organ = organ;
    }

    protected virtual void Win()
    {
        playerHand.EnableHand();
        organ.SetFinished();
        onWin?.Invoke();
        Destroy(gameObject);
    }

    protected virtual void Lose()
    {
        playerHand.EnableHand();
        onLost?.Invoke();
        Destroy(gameObject);
    }
}
