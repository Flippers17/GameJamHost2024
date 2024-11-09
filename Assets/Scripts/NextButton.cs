using System.Collections;
using UnityEngine;

public class NextButton : PickUpable
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveAwayAnimTime = 2;

    [SerializeField][Tooltip("The time it takes for the animation that takes away the body")] 
    private float higherAnimTime = 1f;
    private float currentLerpTime = 0;

    private bool moveAway = false;
    private bool active = false;

    public new void OnEnable()
    {
        canBePickedUp = false;
        updateOnPickUp = true;
    }

    public override void OnPickUp()
    {
        //if (active)
        //    return;

        //if (!moveAway)
        //{
        //    moveAway = true;
        //}
        //else
        //{
        //    moveAway = false;
        //}

        //active = true;
        //currentLerpTime = 0;

        animator.SetBool("GettingNewCorpse", true);
    }

    //private void Update()
    //{
    //    if (!active)
    //        return;

    //    if (moveAway && lerpMoveTime > currentLerpTime)
    //    {
    //        table.position = Vector3.Lerp(table.position, tableAwayPos.position, currentLerpTime);

    //        currentLerpTime += Time.deltaTime;
    //    }
    //    else if (!moveAway && lerpMoveTime > currentLerpTime)
    //    {
    //        table.position = Vector3.Lerp(table.position, tablePos.position, currentLerpTime);

    //        currentLerpTime += Time.deltaTime;
    //    }
    //    else
    //    {
    //        Debug.Log("Done!");
    //        active = false;
    //    }
    //}

    public override void OnDrop()
    {
    }
}
