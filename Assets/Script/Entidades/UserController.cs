using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UserController : MonoBehaviour
{
    // ATRIBUTOS

    [SerializeField] public List<GameObject> ejercito;
    [SerializeField] public bool esMiTurno;
    [SerializeField] private List<GameObject> ejercitoEnemigo;

    // GETTERS & SETTERS

    private void Update()
    {
        foreach(var personaje in ejercito)
        {
            float distance = Mathf.Infinity;
            GameObject posibleObjetivo = null;
            foreach(var enemigo in ejercitoEnemigo)
            {
                if(Vector3.Distance(enemigo.transform.position, personaje.transform.position) < distance)
                {
                    posibleObjetivo = enemigo;
                    distance = Vector3.Distance(enemigo.transform.position, personaje.transform.position);
                }
            }

            personaje.GetComponent<PersonajeController>().setEnemigoObjetivo(posibleObjetivo);
        }
    }


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
