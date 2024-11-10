using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "NewCorpsePort")]
public class NewCorpsePort : ScriptableObject
{
    public UnityAction onNewCorpse;

    public void InvokePort() => onNewCorpse?.Invoke();
}
