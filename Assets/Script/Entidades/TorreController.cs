using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreController : MonoBehaviour
{

    bool vacia = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<BTRanger>() != null)
        {
            if (vacia)
            {
                vacia = false;
                var posicion = collision.transform.position;
                collision.transform.position = new Vector3(posicion.x,2,posicion.z);


            }
        }
    }
}
