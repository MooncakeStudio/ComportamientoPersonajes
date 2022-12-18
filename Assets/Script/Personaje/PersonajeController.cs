using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    // ATRIBUTOS

    private Personaje personaje;

    private GameObject enemigoObjetivo;

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

    public void pidiendoAuxilio() { necesitoAuxilio = true; Debug.Log("Estoy Pidiendo auxilio"); }
    public void noMasAuxilio() { necesitoAuxilio = false; }

    public float GetVelocidad() { return velocidad; }

    public void setEnemigoObjetivo(GameObject enemigoObjetivo) { this.enemigoObjetivo = enemigoObjetivo; }
    public GameObject getEnemigoObjetivo() { return enemigoObjetivo; }
    // METODOS

    private void Awake()
    {
        this.personaje = new Mele();
    }

    private void Start()
    {
        StartCoroutine(cargarEspecial());
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
