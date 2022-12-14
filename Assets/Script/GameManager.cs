using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject user;
    bool esTurno = true;

    // Update is called once per frame
    void Update()
    {
        if (esTurno)
        {
            CambiaTurno();
            StartCoroutine(Actuar());
        }
    }

    public void CambiaTurno()
    {
        esTurno = false;
    }

    IEnumerator Actuar()
    {
        user.GetComponent<UserController>().MoverEjercito();
        yield return new WaitForSeconds(3);
        esTurno = true;
    }
}
