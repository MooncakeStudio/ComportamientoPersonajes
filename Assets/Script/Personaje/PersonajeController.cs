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

    // GETTERS & SETTERS

    public Personaje GetPersonaje() { return this.personaje; }

    public void SetPersonaje(Personaje personaje) { this.personaje = personaje; }

    public bool siendoProvocado() { return provocado; }

    public bool alguienPidiendoAuxilio() { return aliadoAuxilio; }

    public bool tengoAtaqueEspecial() { return especialCargado; }

    public void setEnemigoObjetivo(GameObject enemigoObjetivo) { this.enemigoObjetivo = enemigoObjetivo; }
    public GameObject getEnemigoObjetivo() { return enemigoObjetivo; }
    // METODOS

    private void Awake()
    {
        this.personaje = new Mele();
    }
}
