using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Objeto
{
    #region Atributos y Getters-Setters

    int vida;
    int posX, posY;

    public int GetVida() { return this.vida; }
    public int GetPosX() { return this.posX; }
    public int GetPosY() { return this.posY;}

    public void SetVida(int vida) { this.vida = vida; }
    public void SetPosX(int x) { this.posX = x; }
    public void SetPosY(int y) { this.posY = y; }

    #endregion

    #region Constructor
    public Objeto(int vida = 5, int posX = 0, int posY = 0)
    { 
        this.vida = vida;
        this.posX = posX;
        this.posY = posY;
    }
    #endregion
}
