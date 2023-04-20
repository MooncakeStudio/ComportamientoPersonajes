using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Objeto
{
    // ATRIBUTOS

    int vida;
    int posX, posY;


    // GETTER Y SETTERS

    public int GetVida() { return this.vida; }
    public int GetPosX() { return this.posX; }
    public int GetPosY() { return this.posY;}

    public void SetVida(int vida) { this.vida = vida; }
    public void SetPosX(int x) { this.posX = x; }
    public void SetPosY(int y) { this.posY = y; }


    // CONTRUCTOR

    public Objeto(int vida = 5, int posX = 0, int posY = 0)
    { 
        this.vida = vida;
        this.posX = posX;
        this.posY = posY;
    }
}
