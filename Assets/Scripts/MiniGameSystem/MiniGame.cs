using UnityEngine;
using UnityEngine.Events;

public abstract class MiniGame : MonoBehaviour
{
    public UnityAction onCompletedSuccesfully;
    public abstract void OnStart(PlayerHandController controller);
}
