using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpable : MonoBehaviour
{
    private Rigidbody rb;

    private void OnEnable()
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
