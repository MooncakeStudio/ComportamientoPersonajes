using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject user;
    [SerializeField] GameObject user2;

    [SerializeField] Grid grid;

    [SerializeField] GameObject objeto;

    bool esTurno = true;
    public static bool hayObj = false;

    public static Vector3 objetivo;

    // Update is called once per frame
    void Start()
    {
            //StartCoroutine(Actuar());
    }

    private void Update()
    {
        if (!hayObj)
        {
            hayObj = true;
            Invoke("CrearObjeto", 3);
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

    public void CrearObjeto()
    {
        var x = 0;
        var z = 0;
        do
        {
            x = UnityEngine.Random.Range(0, grid.GetGrid().GetLength(0));
            z = UnityEngine.Random.Range(0, grid.GetGrid().GetLength(1));
        } while (!grid.GetGrid()[x, z].transitable);
        

        Vector3 posicion = grid.GetPosicionGlobal(x, z);

        posicion.y = 0.02f;

        var instance = Instantiate(objeto,posicion,Quaternion.identity);
        //instance.transform.position = posicion;

        objetivo = posicion;
    }
}
