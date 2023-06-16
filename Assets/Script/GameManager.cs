using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Atributos y Getters-setters
    [SerializeField] GameObject user;
    [SerializeField] GameObject user2;

    [SerializeField] Grid worldGrid;
    public static Grid grid;

    [SerializeField] PathFinding a_est;
    public static PathFinding pf;

    [SerializeField] GameObject objeto;

    bool esTurnoUno = true;
    public static bool turnoActivo = false;
    public static bool hayObj = false;

    public static Vector3 objetivo;

    public static Grid GetGrid() { return grid; }
    #endregion

    #region Metodos

    private void Awake()
    {
        grid = worldGrid;
        pf = a_est;
    }
    private void Update()
    {
        /*if (!hayObj)
        {
            hayObj = true;
            Invoke("CrearObjeto", 3);
        }*/

        Actuar();
    }
    public void Actuar()
    {
        if (!turnoActivo)
        {
            if (esTurnoUno)
            {
                Debug.Log("El primero");
                turnoActivo = true;
                user.GetComponent<UserController>().CkeckObjetivos();
                //StartCoroutine(user.GetComponent<UserController>().MoverSoldados(0, user.GetComponent<UserController>().ejercito.Count));
                user.GetComponent<UserController>().MoverSoldados(0, user.GetComponent<UserController>().ejercito.Count);
                //yield return new WaitForSeconds(1);
                esTurnoUno = false;
            }
            else
            {
                Debug.Log("El segundo");
                turnoActivo = true;
                user2.GetComponent<UserController>().CkeckObjetivos();
                //StartCoroutine(user2.GetComponent<UserController>().MoverSoldados(0, user.GetComponent<UserController>().ejercito.Count));
                user2.GetComponent<UserController>().MoverSoldados(0, user2.GetComponent<UserController>().ejercito.Count);
                //yield return new WaitForSeconds(1);
                esTurnoUno = true;
            }
        }
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
        instance.GetComponent<ObjectController>().SetObjeto(new Objeto());
        grid.GetGrid()[x, z].SetObjeto(instance.GetComponent<ObjectController>());
        objetivo = posicion;
    }

    #endregion
}
