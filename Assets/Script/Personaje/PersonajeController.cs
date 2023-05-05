using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersonajeController : MonoBehaviour
{
    // ATRIBUTOS

    protected Personaje personaje;

    protected GameObject enemigoObjetivo;
    protected GameObject aliadoCercano;
    protected Celda objetivo;

    protected bool provocado = false;
    protected bool aliadoAuxilio = false;
    protected bool especialCargado = false;
    protected bool necesitoAuxilio = false;


    [SerializeField] protected float velocidad;


    // GETTERS & SETTERS

    public Personaje GetPersonaje() { return personaje; }

    public void SetPersonaje(Personaje personaje) { this.personaje = personaje; }

    public bool siendoProvocado() { return provocado; }

    public bool alguienPidiendoAuxilio() { return aliadoAuxilio; }

    public bool tengoAtaqueEspecial() { return especialCargado; }

    public bool pidoAuxilio() { return necesitoAuxilio; }

    public void pidiendoAuxilio() { necesitoAuxilio = true; }
    public void noMasAuxilio() { necesitoAuxilio = false; }

   

    public float GetVelocidad() { return velocidad; }

    public void setEnemigoObjetivo(GameObject enemigoObjetivo) { this.enemigoObjetivo = enemigoObjetivo; }

    public void setAliadoCercano(GameObject aliadoCercano) { this.aliadoCercano= aliadoCercano; }
    public GameObject getAliadoCercano() { return aliadoCercano; }
    public GameObject getEnemigoObjetivo() { return enemigoObjetivo; }


    // METODOS

    virtual protected void Awake()
    {
        this.personaje = new Personaje();
    }

    virtual protected void Start()
    {
        personaje.SetFaccion(gameObject.tag);
        Debug.Log(personaje.GetFaccion());
        StartCoroutine(cargarEspecial());


        int x = Random.Range(0, GameManager.grid.GetAncho());
        int y = Random.Range(0, GameManager.grid.GetAlto());

        while (!GameManager.grid.GetGrid()[x, y].transitable || GameManager.grid.GetGrid()[x, y].isOccupied())
        {
            x = Random.Range(0, GameManager.grid.GetAncho());
            y = Random.Range(0, GameManager.grid.GetAlto());
        }

        personaje.x = x;
        personaje.y = y;

        gameObject.transform.position = GameManager.grid.GetPosicionGlobal(x, y);


        GameManager.grid.GetGrid()[personaje.GetX(), personaje.GetY()].SetPersonaje(GetComponent<PersonajeController>());
    }

    virtual protected void FixedUpdate()
    {
        if (objetivo != null)
        {

            transform.position = Vector3.MoveTowards(transform.position, GameManager.grid.GetPosicionGlobal(objetivo.xGrid, objetivo.yGrid), 1 * Time.deltaTime);

            if (Vector3.Distance(transform.position, GameManager.grid.GetPosicionGlobal(objetivo.xGrid, objetivo.yGrid)) < 0.05f)
            {
                GameManager.grid.GetGrid()[personaje.GetX(), personaje.GetY()].SetPersonaje(null);

                

                int x = objetivo.xGrid;
                int y = objetivo.yGrid;


                personaje.SetX(x);
                personaje.SetY(y);


                GameManager.grid.GetGrid()[x, y].SetPersonaje(GetComponent<PersonajeController>());

                objetivo = null;
            }
        }
    }

    public void Moverse(Vector3 objetivo)
    {
        Debug.Log(objetivo);
        GameManager.pf.EncuentraCamino(transform.position, objetivo);
        List<Celda> camino = GameManager.pf.ObtenerCamino(transform.position, objetivo);

        if (camino.Count > 0)
        {
            camino.Reverse();

            this.objetivo = camino[0];
        }
    }

    IEnumerator cargarEspecial()
    {
        if (!especialCargado)
        {
            yield return new WaitForSeconds(5);
            especialCargado = true;
        }
        else
        {

        }

        yield return new WaitForSeconds(2);
        StartCoroutine(cargarEspecial());
    }
}
