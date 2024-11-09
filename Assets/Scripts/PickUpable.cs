using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : MonoBehaviour
{
    public bool canBePickedUp = true;

    public bool UpdateOnPickUp => updateOnPickUp;
    protected bool updateOnPickUp = false;

    private Rigidbody rb;

    public void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void OnPickUp()
    {
        rb.isKinematic = true;
    }
    
    public virtual void OnDrop()
    {
        rb.isKinematic = false;
    }
}
