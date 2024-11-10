using System.Collections;
using UnityEngine;

public class NextButton : PickUpable
{
    [SerializeField] private OrganSpawner corpsePrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveAwayAnimTime = 2;

    private bool active = false;

    public new void OnEnable()
    {
        canBePickedUp = false;
        updateOnPickUp = true;
    }

    public override void OnPickUp()
    {
        if (active)
            return;

        StartCoroutine(GetNewCorpse());
    }

    private IEnumerator GetNewCorpse()
    {
        active = true;
        animator.SetBool("GettingNewCorpse", true);

        yield return new WaitForSeconds(moveAwayAnimTime);

        if(parent.childCount > 0)
            Destroy(parent.GetChild(0).gameObject);

        Vector3 spawnPos = new Vector3(parent.position.x, parent.position.y, parent.position.z);
        OrganSpawner spawner = Instantiate(corpsePrefab, spawnPos, corpsePrefab.transform.rotation);
        spawner.RandomizeOrgan();
        spawner.transform.parent = parent;


        animator.SetBool("GettingNewCorpse", false);
        active = false;
    }

    public override void OnDrop()
    {
    }
}
