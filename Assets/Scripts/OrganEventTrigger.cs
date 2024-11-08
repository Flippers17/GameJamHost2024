using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TriggerType
{
    Hand,
    Bonesaw
}

public class OrganEventTrigger : MonoBehaviour
{
    [SerializeField]
    private TriggerType _triggerType;

    [SerializeField]
    private Transform _triggerPoint;
    [SerializeField]
    private Vector3 _size;

    public void TryGetOrgan()
    {
        Collider[] possibleOrgans = Physics.OverlapBox(_triggerPoint.position, _size);

        for(int i = 0; i < possibleOrgans.Length; i++)
        {
            //if (possibleOrgans[i].TryGetComponent(out Organ organ))
            //{
            //    if(organ.triggerType == _triggerType)
            //        //trigger
            //}
        }
    }
}
