using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    // ATRIBUTOS

    private Personaje personaje;


    // GETTERS & SETTERS

    public Personaje GetPersonaje() { return this.personaje; }

    public void SetPersonaje(Personaje personaje) { this.personaje = personaje; }


    // METODOS

    private void Awake()
    {
        this.personaje = new Mele();
    }
}
