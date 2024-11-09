using UnityEngine;
using UnityEngine.Events;

public class VeinMiniGame : MiniGame
{
    [SerializeField] private Mesh veinCutMesh;
    [SerializeField] private ParticleSystem bloodSpurt;

    public bool Clicked => m_Clicked;
    private bool m_Clicked = false;
    
    public UnityAction OnClicked;

    public override void OnStart(PlayerHandController controller, Organ organ)
    {
        OnClicked?.Invoke();

        GetComponent<MeshFilter>().mesh = veinCutMesh;

        transform.parent = null;
        m_Clicked = true;
        bloodSpurt.Play();

        return;
    }
}
