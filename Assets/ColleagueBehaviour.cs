using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class ColleagueBehaviour : MonoBehaviour
{
    [SerializeField]
    private WarningSignPort warningSignPort;

    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Transform _jumpscarePos;

    private void OnEnable()
    {
        warningSignPort.OnStageUpdate += SetState;
    }

    private void Start()
    {
        GameManager.instance.OnPlayerCaught.AddListener(JumpScare);
    }

    private void OnDisable()
    {
        warningSignPort.OnStageUpdate -= SetState;
    }


    private void SetState(int state)
    {
        _anim.SetInteger("State", state);
    }

    private void JumpScare()
    {
        if (_jumpscarePos)
        {
            transform.position = _jumpscarePos.position;
            transform.rotation = _jumpscarePos.rotation;
        }
        
        _anim.SetTrigger("Jumpscare");
    }

    public void LookTowardsPlayer()
    {

    }
}


