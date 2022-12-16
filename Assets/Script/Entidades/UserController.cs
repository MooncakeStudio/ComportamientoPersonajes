using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UserController : MonoBehaviour
{
    // ATRIBUTOS

    [SerializeField] public List<GameObject> ejercito;
    [SerializeField] public bool esMiTurno;


    // GETTERS & SETTERS




    // METODOS

    public void MoverEjercito()
    {
         
    }

    public IEnumerator MoverSoldados(int contador, int maxSoldados)
    {
        contador++;

        yield return new WaitForSeconds(1);

        if(contador < maxSoldados)
        {
            MoverSoldados(contador, maxSoldados);
        }
    }
}
