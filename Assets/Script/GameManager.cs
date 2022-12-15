using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject user;
    [SerializeField] GameObject user2;

    [SerializeField] GameObject objeto;

    bool esTurno = true;
    public static bool hayObj = false;

    public static Vector3 objetivo;

    // Update is called once per frame
    void Start()
    {
            StartCoroutine(Actuar());
    }

    private void Update()
    {
        if (!hayObj)
        {
            hayObj = CrearObjeto();
        }
    }

    IEnumerator Actuar()
    {
        if (esTurno)
        {
            user.GetComponent<UserController>().MoverSoldados(0, user.GetComponent<UserController>().ejercito.Count);
            yield return new WaitForSeconds(1);
            esTurno = false;
        }
        else
        {
            user2.GetComponent<UserController>().MoverSoldados(0, user.GetComponent<UserController>().ejercito.Count);
            yield return new WaitForSeconds(1);
            esTurno = true;
        }

        //if (esTurno)
        //{
        //    user.GetComponent<UserController>().MoverEjercito();
        //    yield return new WaitForSeconds(1);
        //    esTurno = false;
        //}
        //else
        //{
        //    user2.GetComponent<UserController>().MoverEjercito();
        //    yield return new WaitForSeconds(1);
        //    esTurno = true;
        //}

        StartCoroutine(Actuar());
    }

    public bool CrearObjeto()
    {
        var x = UnityEngine.Random.Range(-85, 86);
        var z = UnityEngine.Random.Range(-85, 86);

        Vector3 posicion = new Vector3(x, 1, z);

        var instance = Instantiate(objeto);
        instance.transform.position = posicion;

        objetivo = posicion;

        return true;
    }
}
