using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    [SerializeField]
    private PlayerInputHandler _input;

    [SerializeField]
    private Transform handPos;

    [SerializeField]
    private Rect boundary;

    [SerializeField]
    private float handHeight = 1f;

    [SerializeField]
    private float handMoveSpeed = 3f;

    [Space(15), SerializeField]
    private float _pickUpRadius = .3f;

    private PickUpHandler _pickUpHandler = new PickUpHandler();


    private void OnEnable()
    {
        handPos.position = new Vector3(boundary.x, handHeight, boundary.y);
        _input.OnPickUp += TryPickup;
    }

    private void Update()
    {
        Vector3 newPos = handPos.position;
        newPos += new Vector3(_input.movementVector.x, 0, _input.movementVector.y) * (handMoveSpeed * Time.deltaTime);

        newPos.x = Mathf.Clamp(newPos.x, boundary.position.x - (boundary.width/2), boundary.position.x + (boundary.width / 2));
        newPos.z = Mathf.Clamp(newPos.z, boundary.position.y - (boundary.height / 2), boundary.position.y + (boundary.height / 2));

        handPos.position = newPos;
    }

    private void TryPickup()
    {
        if (_pickUpHandler.holdingItem)
        {
            DropItem();
            return;
        }

        Collider[] objects = Physics.OverlapSphere(handPos.position, _pickUpRadius);

        for(int i = 0; i < objects.Length; i++)
        {
            if(objects[i].TryGetComponent(out PickUpable item))
            {
                PickUpItem(item);
                return;
            }
        }
    }

    private void PickUpItem(PickUpable item)
    {
        if (_pickUpHandler.holdingItem)
            return;

        _pickUpHandler.holdingItem = true;
        _pickUpHandler.currentItem = item;
    }

    private void DropItem()
    {

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(boundary.position.x, handHeight, boundary.position.y), new Vector3(boundary.width, 1, boundary.height));
    }
}
