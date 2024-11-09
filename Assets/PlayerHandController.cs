using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    public Transform HandTransform => handPos;

    [SerializeField]
    private PlayerInputHandler _input;

    [SerializeField]
    private Transform handPos;
    [SerializeField]
    private Transform _handMesh;
    [SerializeField]
    private Vector3 _meshOffset;
    [SerializeField]
    private Animator _handAnim;


    [Space(15),SerializeField]
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
        _input.OnInteract += TryInteract;
        _input.OnDrop += DropItem;
    }

    private void OnDisable()
    {
        _input.OnInteract -= TryInteract;
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

        _handMesh.transform.position = handPos.position + _meshOffset;
    }


    public void TryInteract()
    {
        //if(!canMoveHand)
        //    return;

        if (_pickUpHandler.holdingItem)
        {
            //DropItem();
            //Trigger minigames and stuff
            if (_pickUpHandler.TryTriggerOrganEvent(this))
            {
                DisableHand();
            }

            return;
        }

        if (_organTrigger.TryGetOrgan(this))
        {
            DisableHand();
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

    public void PickUpItem(PickUpable item)
    {
        if (_pickUpHandler.holdingItem || !canMoveHand || !item.canBePickedUp)
        {    
            if (item.UpdateOnPickUp)
                item.OnPickUp();
            return;
        }

        _handAnim.SetBool("Closed", true);
        _pickUpHandler.PickUpItem(item);
    }

    public void DropItem()
    {
        if (!_pickUpHandler.holdingItem || !canMoveHand)
            return;

        _handAnim.SetBool("Closed", false);
        _pickUpHandler.DropItem();
    }


    public void DisableHand()
    {
        if (!canMoveHand)
            return;
        canMoveHand = false;
        _input.OnInteract -= TryInteract;
    }

    public void EnableHand()
    {
        if(canMoveHand)
            return ;
        canMoveHand = true;
        _input.OnInteract += TryInteract;
    }


    public void PlayMinigameAnimation(string stateName)
    {
        if(stateName == "")
            return;

        _handAnim.SetBool("In Minigame", true);
        
        _handAnim.Play(stateName, 0);
    }

    public void WinMinigameAnim()
    {
        _handAnim.SetBool("In Minigame", false);
        _handAnim.SetTrigger("Win Minigame");
    }
    
    public void LoseMiniagemAnim()
    {
        _handAnim.SetBool("In Minigame", false);
        _handAnim.SetTrigger("Lose Minigame");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(boundary.position.x, handHeight, boundary.position.y), new Vector3(boundary.width, 1, boundary.height));

        Gizmos.DrawWireSphere(handPos.position, _pickUpRadius);
    }
}
