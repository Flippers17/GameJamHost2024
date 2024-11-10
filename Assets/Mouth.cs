using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private ParticleSystem bloodSplat;
    [SerializeField] private AudioSource eatingAudio;

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

        if(collision.TryGetComponent(out Organ organ))
        {
            if (m_PlayerHandController)
                m_PlayerHandController.DropItem();

            pointsManager.AddPoints(organ.Points);
            bloodSplat.Play();
            eatingAudio.Play();
            Destroy(organ.gameObject);
        }
    }
}
