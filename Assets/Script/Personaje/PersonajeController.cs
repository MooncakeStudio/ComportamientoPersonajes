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

    bool provocado = false;
    bool aliadoAuxilio = false;
    bool especialCargado = false;
    bool necesitoAuxilio = false;


    [SerializeField] float velocidad;


    // GETTERS & SETTERS

    public Personaje GetPersonaje() { return this.personaje; }

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

        this.personaje.x = x;
        this.personaje.y = y;

        this.gameObject.transform.position = GameManager.grid.GetPosicionGlobal(x, y);


        GameManager.grid.GetGrid()[this.personaje.GetX(), this.personaje.GetY()].SetPersonaje(GetComponent<PersonajeController>());
    }

    virtual protected void FixedUpdate()
    {
        if (this.objetivo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.grid.GetPosicionGlobal(this.objetivo.xGrid, this.objetivo.yGrid), 1 * Time.deltaTime);

            if (Vector3.Distance(transform.position, GameManager.grid.GetPosicionGlobal(this.objetivo.xGrid, this.objetivo.yGrid)) < 0.05f)
            {
                GameManager.grid.GetGrid()[this.personaje.GetX(), this.personaje.GetY()].SetPersonaje(null);

                int x = this.objetivo.xGrid;
                int y = this.objetivo.yGrid;


                this.personaje.SetX(x);
                this.personaje.SetY(y);


                GameManager.grid.GetGrid()[x, y].SetPersonaje(GetComponent<PersonajeController>());

                this.objetivo = null;
            }
        }
    }

    public void Moverse(Vector3 objetivo)
    {
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
