using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje
{
    // ATRIBUTOS

    protected int vida;
    protected int vidaMax;
    protected int ataque;
    protected string faccion;

    public int x;
    public int y;

    protected bool inspirado;

    //Constructor
    public Personaje(int vida, int ataque, string faccion) { this.vida = vida; this.ataque = ataque; this.faccion = faccion; }
    public Personaje() { vida = 30; ataque = 10; faccion = ""; x = 0; y = 0; }
    public Personaje(int vida, int ataque, string faccion, int x, int y)
    {
        this.vida = vida;
        this.vidaMax = vida;
        this.ataque = ataque;
        this.faccion = faccion;
        this.x = x;
        this.y = y;
    }


    // GETTERS & SETTERS

    public int GetVida() { return vida; }
    public int GetAtaque() { return ataque; }
    public int GetX() { return x; }
    public int GetY() { return y; }
    public string GetFaccion() { return faccion; }
    public bool GetInspirado() { return inspirado; }

    public void SetVida(int vida) { this.vida = vida; }
    public void SetAtaque(int ataque) { this.ataque = ataque; }
    public void SetX(int x) { this.x = x; }
    public void SetY(int y) { this.y = y; }
    public void SetFaccion(string faccion) { this.faccion = faccion; }
    public void SetInspirado(bool inspirado) { this.inspirado = inspirado; }

    public void Herida(int damage)
    {
        this.vida -= damage;
        if (vida <= 0)
            vida = 0;
    }

    // METODOS

    virtual public int Atacar(PersonajeController enemigo)
    {
        int ataqueAux = this.ataque;

        if (inspirado)
        {
            ataqueAux = (int)Mathf.Ceil((float)(ataqueAux * 1.5f));
            inspirado = false;
        }

        var enemigoAux = enemigo.GetPersonaje();
        enemigoAux.Herida(ataqueAux);

        return enemigo.GetPersonaje().GetVida();
    }

    virtual public PersonajeController EnemigoARango() { return null; }
}
