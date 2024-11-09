using System.Collections;
using UnityEngine;

public class NextButton : PickUpable
{
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



        animator.SetBool("GettingNewCorpse", false);
        active = false;
    }

    public override void OnDrop()
    {
    }
}
