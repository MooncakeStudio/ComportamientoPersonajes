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
    [SerializeField] private List<GameObject> enemigosProvocando = new List<GameObject>();
    private List<GameObject> aliadosAuxilio = new List<GameObject>();
    int aliadoPidiendoAuxilio = 0;
    GameObject aliadoProblemas;

    public UnityEvent aliadoPideAuxilio = new UnityEvent();
    int turnosUsados = 0;


    // GETTERS & SETTERS

    private void OnEnable()
    {
        MeleeController.provocandoEvent += PercepcionPersonajeProvocando;
        PersonajeController.PidiendoAuxilioEvent += PercepcionPersonajeAuxilio;
        PersonajeController.NoPidiendoAxuilioEvent += PercepcionPersonajeNoAuxilio;
        PersonajeController.MuertoEvent += ElminiarPersonaje;
    }

    private void OnDisable()
    {
        MeleeController.provocandoEvent -= PercepcionPersonajeProvocando;
        PersonajeController.PidiendoAuxilioEvent -= PercepcionPersonajeAuxilio;
        PersonajeController.NoPidiendoAxuilioEvent -= PercepcionPersonajeNoAuxilio;
        PersonajeController.MuertoEvent -= ElminiarPersonaje;
    }

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
            enemigosProvocando.Clear();
            GameManager.turnoActivo = false;
            turnosUsados = 0;
        }
    }

    public int GetPersonajes() { return ejercito.Count; }

    public void turnoFinalizado() 
    { 
        turnosUsados++;
        if(turnosUsados < ejercito.Count)
        {
            CkeckObjetivos();
            ejercito[turnosUsados].GetComponent<BTAbstracto>().GetBT().Active = true;
        }
    }

    // METODOS

    public void CkeckObjetivos()
    {
        foreach(var personaje in ejercito)
        {

            float distance = Mathf.Infinity;
            GameObject posibleObjetivo = null;
            if(enemigosProvocando.Count > 0)
            {
                personaje.GetComponent<PersonajeController>().SetEnemigoProvocando(true);

                foreach (var enemigo in enemigosProvocando)
                {
                    if (Vector3.Distance(enemigo.transform.position, personaje.transform.position) < distance)
                    {
                        posibleObjetivo = enemigo;
                        distance = Vector3.Distance(enemigo.transform.position, personaje.transform.position);
                    }
                }
                //enemigosProvocando.Remove(posibleObjetivo);
            }
            else
            {
                personaje.GetComponent<PersonajeController>().SetEnemigoProvocando(false);
                foreach (var enemigo in ejercitoEnemigo)
                {
                    if (Vector3.Distance(enemigo.transform.position, personaje.transform.position) < distance)
                    {
                        posibleObjetivo = enemigo;
                        distance = Vector3.Distance(enemigo.transform.position, personaje.transform.position);
                    }
                }
            }
            personaje.GetComponent<PersonajeController>().setEnemigoObjetivo(posibleObjetivo);

            distance = Mathf.Infinity;
            if(aliadosAuxilio.Count > 0)
            {
                foreach (var aliado in aliadosAuxilio)
                {
                    if (!aliado.name.Equals(personaje.name))
                    {
                        if (Vector3.Distance(aliado.transform.position, personaje.transform.position) < distance)
                        {
                            posibleObjetivo = aliado;
                            distance = Vector3.Distance(aliado.transform.position, personaje.transform.position);
                        }
                    }
                }
            }
            else
            {
                foreach (var aliado in ejercito)
                {
                    if (!aliado.name.Equals(personaje.name))
                    {
                        if (Vector3.Distance(aliado.transform.position, personaje.transform.position) < distance)
                        {
                            posibleObjetivo = aliado;
                            distance = Vector3.Distance(aliado.transform.position, personaje.transform.position);
                        }
                    }
                    
                }
            }

            if (posibleObjetivo.tag.Equals(ejercitoEnemigo[0].tag))
            {
                foreach (var aliado in ejercito)
                {
                    if (!aliado.name.Equals(personaje.name))
                    {
                        if (Vector3.Distance(aliado.transform.position, personaje.transform.position) < distance)
                        {
                            posibleObjetivo = aliado;
                            distance = Vector3.Distance(aliado.transform.position, personaje.transform.position);
                        }
                    }

                }
            }
            

            personaje.GetComponent<PersonajeController>().setAliadoCercano(posibleObjetivo);
        }
    }

    public void MoverSoldados(int contador, int maxSoldados)
    {
        ejercito[turnosUsados].GetComponent<BTAbstracto>().GetBT().Active = true;
    }

    private void PercepcionPersonajeProvocando(GameObject sender)
    {
        if (!sender.CompareTag(gameObject.tag))
        {
            enemigosProvocando.Add(sender);
        }
    }

    private void PercepcionPersonajeAuxilio(GameObject sender)
    {
        if(sender.CompareTag(gameObject.tag)) 
        {
            var paraAdd = true;
            if(aliadosAuxilio.Count > 0)
            {
                foreach(var personaje in aliadosAuxilio)
                {
                    if (personaje.name.Equals(sender.name))
                        paraAdd = false;
                }
            }

            if(paraAdd)
                aliadosAuxilio.Add(sender);

            foreach (var personaje in ejercito)
                personaje.GetComponent<PersonajeController>().SetAliadoPidiendoAuxilio(true);
        }
    }

    private void PercepcionPersonajeNoAuxilio(GameObject sender)
    {
        GameObject personajeEliminar = null;
        if (sender.CompareTag(gameObject.tag))
        {
            if(aliadosAuxilio.Count > 0)
            {
                foreach(var personaje in aliadosAuxilio)
                {
                    if (sender.name.Equals(personaje.name))
                        personajeEliminar = personaje;
                }

                aliadosAuxilio.Remove(personajeEliminar);
                if(aliadosAuxilio.Count < 0)
                {
                    foreach (var personaje in ejercito)
                        personaje.GetComponent<PersonajeController>().SetAliadoPidiendoAuxilio(false);
                }
            }

        }
    }

    public void ElminiarPersonaje(GameObject sender)
    {
        if (sender != null)
        {
            GameObject personajeEliminar = null;
            if (sender.CompareTag(gameObject.tag))
            {
                if (ejercito.Count > 0)
                {
                    foreach (var personaje in ejercito)
                    {
                        if (sender.name.Equals(personaje.name))
                            personajeEliminar = personaje;
                    }

                    aliadosAuxilio.Clear();

                    GameManager.grid.GetGrid()[personajeEliminar.GetComponent<PersonajeController>().GetPersonaje().GetX(),
                        personajeEliminar.GetComponent<PersonajeController>().GetPersonaje().GetY()].SetPersonaje(null);

                    if (turnosUsados > 0) turnoFinalizado();
                    ejercito.Remove(personajeEliminar);
                    if (ejercito.Count <= 0)
                        GameManager.finPartida = true;
                    //ejercito[0].GetComponent<BTAbstracto>().GetBT().Active = true;
                }
            }
            else
            {
                if (ejercitoEnemigo.Count > 0)
                {
                    foreach (var personaje in ejercitoEnemigo)
                    {
                        if (sender.name.Equals(personaje.name))
                            personajeEliminar = personaje;
                    }

                    ejercitoEnemigo.Remove(personajeEliminar);
                }
            }
        }
        //CkeckObjetivos();
    }
}
