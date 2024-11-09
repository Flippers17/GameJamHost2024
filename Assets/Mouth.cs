using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField] private PointsManager pointsManager;

    private PlayerHandController m_PlayerHandController;

    private void Start()
    {
        m_PlayerHandController = FindAnyObjectByType<PlayerHandController>();

        if(!m_PlayerHandController)
            Debug.LogWarning("No " + typeof(PlayerHandController).Name + " in scene!", this);
        if(!pointsManager)
            Debug.LogWarning("No " + typeof(PointsManager).Name + " on object!", this);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (m_PlayerHandController)
            m_PlayerHandController.DropItem();

        if(collision.TryGetComponent(out Organ organ))
        {
            pointsManager.AddPoints(organ.Points);
            Destroy(organ.gameObject);
        }
    }
}
