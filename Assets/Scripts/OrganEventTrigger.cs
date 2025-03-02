using UnityEngine;

public enum ToolType
{
    Hand,
    Bonesaw,
    scalpel
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
            else
            {
                PopUpTextHandler.Instance.SetText("You need a " + closestOrgan.TriggerType + " to take this", 5f);
            }
        }

        return false;
    }



    private void OnDrawGizmosSelected()
    {
        if (!_triggerPoint)
            return;

        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(_triggerPoint.position, _size);
    }
}
