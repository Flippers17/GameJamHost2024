using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickUpHandler
{
    [HideInInspector]
    public PickUpable currentItem;

    public float lerpSpeed = 5;

    [HideInInspector]
    public bool holdingItem = false;

    public void PickUpItem(PickUpable item)
    {
        currentItem = item; 
        holdingItem = true;

        currentItem.OnPickUp();
    }

    public void DropItem()
    {
        currentItem.OnDrop();
        currentItem = null;
        holdingItem = false;

    }


    public void MoveItem(Vector3 position)
    {
        if(!holdingItem)
            return;

        currentItem.transform.position = Vector3.Lerp(currentItem.transform.position, position, lerpSpeed * Time.deltaTime);
    }
}
