using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    // ATRIBUTOS

    private Personaje personaje;
    bool provocado;
    bool aliadoAuxilio;
    bool especialCargado;

    // GETTERS & SETTERS

    public Personaje GetPersonaje() { return this.personaje; }

    public void SetPersonaje(Personaje personaje) { this.personaje = personaje; }

    public bool siendoProvocado() { return provocado; }

    public bool alguienPidiendoAuxilio() { return aliadoAuxilio; }

    public bool tengoAtaqueEspecial() { return especialCargado; }
    // METODOS

    private void Awake()
    {
        this.personaje = new Mele();
    }
}
