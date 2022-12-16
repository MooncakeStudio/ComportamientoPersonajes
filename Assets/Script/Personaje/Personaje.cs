using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje
{
    // ATRIBUTOS

    protected int vida;
    protected int ataque;

    protected int x;
    protected int y;

    // GETTERS & SETTERS

    public int GetVida() { return vida; }
    public int GetAtaque() { return ataque; }

    public void SetVida(int vida) { this.vida = vida; }
    public void SetAtaque(int ataque) { this.ataque = ataque; }

    // METODOS

    virtual public void Atacar(PersonajeController enemigo) { }

    virtual public void UsarEspecial() { }

    virtual public Personaje EnemigoARango() { return null; }
}
