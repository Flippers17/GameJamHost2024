using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Organ organ))
        {

        }
    }
}
