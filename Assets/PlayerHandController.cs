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
    private OrganEventTrigger _organTrigger;

    [SerializeField]
    private Rect boundary;

    [SerializeField]
    private float handHeight = 1f;

    [SerializeField]
    private float handMoveSpeed = 3f;

    [Space(15), SerializeField]
    private float _pickUpRadius = .3f;

    [SerializeField]
    private PickUpHandler _pickUpHandler = new PickUpHandler();

    public bool canMoveHand = true;


    private void OnEnable()
    {
        handPos.position = new Vector3(boundary.x, handHeight, boundary.y);
        _input.OnInteract += TryPickup;
        _input.OnDrop += DropItem;
    }

    private void OnDisable()
    {
        _input.OnInteract -= TryPickup;
        _input.OnDrop -= DropItem;
    }

    private void Update()
    {
        if (canMoveHand)
            MoveHand(_input.movementVector * handMoveSpeed);
    }


    public void MoveHand(Vector2 movement)
    {
        Vector3 newPos = handPos.position;
        newPos += new Vector3(movement.x, 0, movement.y) * Time.deltaTime;

        newPos.x = Mathf.Clamp(newPos.x, boundary.position.x - (boundary.width / 2), boundary.position.x + (boundary.width / 2));
        newPos.z = Mathf.Clamp(newPos.z, boundary.position.y - (boundary.height / 2), boundary.position.y + (boundary.height / 2));

        handPos.position = newPos;

        _pickUpHandler.MoveItem(newPos);
    }


    private void TryPickup()
    {
        if(!canMoveHand)
            return;

        if (_pickUpHandler.holdingItem)
        {
            //DropItem();
            //Trigger minigames and stuff
            if (_pickUpHandler.TryTriggerOrganEvent(this))
            {
                canMoveHand = false;
            }

            return;
        }

        if (_organTrigger.TryGetOrgan(this))
        {
            canMoveHand = false;
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
        if (_pickUpHandler.holdingItem || !canMoveHand)
            return;

        _pickUpHandler.PickUpItem(item);
    }

    private void DropItem()
    {
        if (!_pickUpHandler.holdingItem || !canMoveHand)
            return;

        _pickUpHandler.DropItem();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(boundary.position.x, handHeight, boundary.position.y), new Vector3(boundary.width, 1, boundary.height));

        Gizmos.DrawWireSphere(handPos.position, _pickUpRadius);
    }
}
