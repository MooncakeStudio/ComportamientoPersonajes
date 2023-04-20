using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celda
{
    // ATRIBUTOS

    public int xGrid, yGrid;

    public bool transitable;

    private PersonajeController personaje = null;
    private ObjectController objeto = null;

    public Celda padre;
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
    public ObjectController GetObjeto() { return this.objeto; }

    public void SetPersonaje(PersonajeController prsonaje) 
    { 
        this.personaje = prsonaje;
        this.transitable = (this.personaje != null) ? false : true;
    }

    public void SetObjeto(ObjectController objeto)
    {
        this.objeto = objeto;
        this.transitable = (this.objeto != null) ? false : true;
    }


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
