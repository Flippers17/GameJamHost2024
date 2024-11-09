using UnityEngine;
using UnityEngine.Events;

public class VeinMiniGame : MiniGame
{
    //[SerializeField] private Mesh veinCutMesh;
    [SerializeField] private ParticleSystem bloodSpurtPrefab;

    public bool Clicked => m_Clicked;
    private bool m_Clicked = false;
    
    public UnityAction OnClicked;

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        OnClicked?.Invoke();

        //GetComponent<MeshFilter>().mesh = veinCutMesh;

        Instantiate(bloodSpurtPrefab, transform.position, Quaternion.identity).Play();
        
        transform.parent = null;
        m_Clicked = true;

        Destroy(gameObject);

        return;
    }
}
