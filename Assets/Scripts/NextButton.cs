using System.Collections;
using UnityEngine;

public class NextButton : PickUpable
{
    [SerializeField] private OrganSpawner corpsePrefab;
    [SerializeField] private Transform table;
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

        if(table.childCount > 0)
            Destroy(table.GetChild(0).gameObject);

        Vector3 spawnPos = new Vector3(table.position.x, corpsePrefab.transform.position.y, table.position.z);
        OrganSpawner spawner = Instantiate(corpsePrefab, spawnPos, corpsePrefab.transform.rotation);
        spawner.RandomizeOrgan();
        spawner.transform.parent = table;


        animator.SetBool("GettingNewCorpse", false);
        active = false;
    }

    public override void OnDrop()
    {
    }
}
