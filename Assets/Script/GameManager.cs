using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ATRIBUTOS

    [SerializeField] GameObject user;
    [SerializeField] GameObject user2;

    [SerializeField] Grid worldGrid;
    static Grid grid;

    [SerializeField] GameObject objeto;

    bool esTurnoUno = true;
    public static bool hayObj = false;

    public static Vector3 objetivo;


    // GETTERS & SETTERS

    public static Grid GetGrid() { return grid; }


    // METODOS

    private void Awake()
    {
        grid = worldGrid;
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
        if (esTurnoUno)
        {
            user.GetComponent<UserController>().MoverSoldados(0, user.GetComponent<UserController>().ejercito.Count);
            yield return new WaitForSeconds(1);
            esTurnoUno = false;
        }
        else
        {
            user2.GetComponent<UserController>().MoverSoldados(0, user.GetComponent<UserController>().ejercito.Count);
            yield return new WaitForSeconds(1);
            esTurnoUno = true;
        }

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

        var instance = Instantiate(objeto, posicion, Quaternion.identity);

        objetivo = posicion;
    }
}
