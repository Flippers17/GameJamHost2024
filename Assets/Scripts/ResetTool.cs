using UnityEngine;

public class ResetTool : MonoBehaviour
{
    [SerializeField] private NewCorpsePort newCorpsePort;
    [SerializeField] private Transform toolTransform;
    [SerializeField] private Transform toolPosition;

    private void OnEnable()
    {
        newCorpsePort.onNewCorpse += ResetObject;
    }

    private void OnDisable()
    {
        newCorpsePort.onNewCorpse -= ResetObject;
    }

    private void ResetObject()
    {
        toolTransform.position = transform.position;
    }
}
