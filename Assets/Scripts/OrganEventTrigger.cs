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


    public bool TryGetOrgan(PlayerHandController controller)
    {
        Collider[] possibleOrgans = Physics.OverlapBox(_triggerPoint.position, _size);

        float closestDistance = float.MaxValue;
        Organ closestOrgan = null;

        for(int i = 0; i < possibleOrgans.Length; i++)
        {
            if (possibleOrgans[i].TryGetComponent(out Organ organ) && !organ.IsMiniGameActive && !organ.Finished)
            {
                float distance = Vector3.Distance(transform.position, organ.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestOrgan = organ;
                }                
            }
        }

        if (closestOrgan)
        {
            if (closestOrgan.TriggerType == _toolType)
            {
                closestOrgan.StartMiniGame(controller);
                return true;
            }
        }

        return false;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(_triggerPoint.position, _size);
    }
}
