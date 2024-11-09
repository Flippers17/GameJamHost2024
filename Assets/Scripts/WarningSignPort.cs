using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="WarningSignPort", menuName ="Warning Sign Port")]
public class WarningSignPort : ScriptableObject
{
    public UnityAction<int> OnStageUpdate;

    public void ChangeStage(int stage)
    {
        OnStageUpdate?.Invoke(stage); 
    }
}
