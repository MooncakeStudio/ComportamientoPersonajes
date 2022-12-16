using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celda
{
    // ATRIBUTOS

    public int xGrid, yGrid;

    public bool transitable;

    public Celda padre;

    private PersonajeController personaje = null;

    public int costeG;
    public int costeH;
    public int costeF 
    { 
        get {
            return costeG + costeH;
        }
    }


    // GETTERS & SETTERS

    public PersonajeController GetPersonaje() { return this.personaje; }

    public void SetPersonaje(PersonajeController prsonaje) { this.personaje = prsonaje; }

    
    // CONSTRUCTOR

    public Celda(int xGrid = 0, int yGrid = 0, bool transitable = true, int costeG = 0, int costeH = 0)
    {
        this.xGrid = xGrid;
        this.yGrid = yGrid;
        this.transitable = transitable;
        this.costeG = costeG;
        this.costeH = costeH;
    }


    // METODOS

    public bool isOccupied()
    {
        return (personaje != null) ? true : false;
    }
}
