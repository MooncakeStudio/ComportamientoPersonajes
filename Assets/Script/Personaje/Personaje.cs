using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje
{
    // ATRIBUTOS

    protected int vida = 3;
    protected int ataque = 10;
    protected string faccion;

    public int x;
    public int y;

    // GETTERS & SETTERS

    public int GetVida() { return vida; }
    public int GetAtaque() { return ataque; }
    public int GetX() { return x; }
    public int GetY() { return y; }
    public string GetFaccion() { return faccion; }

    public void SetVida(int vida) { this.vida = vida; }
    public void SetAtaque(int ataque) { this.ataque = ataque; }
    public void SetX(int x) { this.x = x; }
    public void SetY(int y) { this.y = y; }
    public void SetFaccion(string faccion) { this.faccion = faccion; }  

    // METODOS

    virtual public void Atacar(PersonajeController enemigo) { }

    virtual public void UsarEspecial() { }

    virtual public PersonajeController EnemigoARango() { return null; }
}
