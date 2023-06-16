using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje
{

    #region Atributos
    protected int vida;
    protected int ataque;
    protected string faccion;

    public int x;
    public int y;
    #endregion

    #region Constructores
    public Personaje(int vida, int ataque, string faccion)
    {
        this.vida = vida;
        this.ataque = ataque;
        this.faccion = faccion;
    }
    public Personaje()
    {
        vida = 30;
        ataque = 10;
        faccion = "";
        x = 0;
        y = 0;
    }
    public Personaje(int vida, int ataque, string faccion, int x, int y)
    {
        this.vida = vida;
        this.ataque = ataque;
        this.faccion = faccion;
        this.x = x;
        this.y = y;
    }
    #endregion

    #region Getters-Setters
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
    #endregion

    #region Metodos
    virtual public void Atacar(PersonajeController enemigo) { }

    virtual public void UsarEspecial() { }

    virtual public PersonajeController EnemigoARango() { return null; }

    #endregion
}
