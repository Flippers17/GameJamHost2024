using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="WarningSignPort", menuName ="Warning Sign Port")]
public class WarningSignPort : ScriptableObject
{
    public UnityAction<int> OnStageUpdate;
    public UnityAction OnDisableSign;
    public UnityAction OnEnableSign;

    public void DisableWarningSign()
    {
        OnDisableSign?.Invoke();
    }
    
    public void EnableWarningSign()
    {
        OnEnableSign?.Invoke();
    }

    public void ChangeStage(int stage)
    {
        OnStageUpdate?.Invoke(stage); 
    }
}
