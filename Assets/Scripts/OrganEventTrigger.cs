using UnityEngine;

public enum ToolType
{
    Hand,
    Bonesaw
}

public class OrganEventTrigger : MonoBehaviour
{
    [SerializeField]
    private ToolType _toolType;

    [SerializeField]
    private Transform _triggerPoint;
    [SerializeField]
    private Vector3 _size;

    public void TryGetOrgan(PlayerHandController controller)
    {
        Collider[] possibleOrgans = Physics.OverlapBox(_triggerPoint.position, _size);

        for(int i = 0; i < possibleOrgans.Length; i++)
        {
            if (possibleOrgans[i].TryGetComponent(out Organ organ))
            {
                if(organ.TriggerType == _toolType)
                {
                    organ.StartMiniGame(controller);
                }
            }
        }
    }
}
