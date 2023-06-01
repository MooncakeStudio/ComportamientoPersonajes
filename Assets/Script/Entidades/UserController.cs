using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class UserController : MonoBehaviour
{
    // ATRIBUTOS

    [SerializeField] public List<GameObject> ejercito;
    [SerializeField] public bool esMiTurno;
    [SerializeField] private List<GameObject> ejercitoEnemigo;
    int aliadoPidiendoAuxilio = 0;
    GameObject aliadoProblemas;

    public UnityEvent aliadoPideAuxilio = new UnityEvent();
    int turnosUsados = 0;

    // GETTERS & SETTERS

    private void Start()
    {
        foreach(var personaje in ejercito)
        {
            personaje.GetComponent<PersonajeController>().Usuario(this);
        }
    }

    private void Update()
    {
        if(turnosUsados >= ejercito.Count)
        {
            GameManager.turnoActivo = false;
            turnosUsados = 0;
        }
    }

    public void turnoFinalizado() { turnosUsados++; }

    // METODOS

    public void CkeckObjetivos()
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

            /*distance = Mathf.Infinity;
            foreach(var aliado in ejercito)
            {
                if (Vector3.Distance(aliado.transform.position, personaje.transform.position) < distance)
                {
                    posibleObjetivo = aliado;
                    distance = Vector3.Distance(aliado.transform.position, personaje.transform.position);
                }
            }

            personaje.GetComponent<PersonajeController>().setAliadoCercano(posibleObjetivo);*/
        }
    }

    public void MoverSoldados(int contador, int maxSoldados)
    {
        //contador++;

        //yield return new WaitForSeconds(1);

        //if(contador < maxSoldados)
        //{
        //    MoverSoldados(contador, maxSoldados);
        //}

        foreach(var personaje in this.ejercito)
        {
            personaje.GetComponent<BTAbstracto>().GetBT().Active = true;
        }
        
        /*while(personajesTurno < ejercito.Count)
        {
            yield return null;
        }*/
       // yield return new WaitForSeconds(0.5f);
        //GameManager.turnoActivo = false;
    }
}
