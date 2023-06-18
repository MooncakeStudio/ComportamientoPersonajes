using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerController : PersonajeController
{
    [SerializeField] private int VidaPoner;
    [SerializeField] private int ataque;

    protected override void Awake()
    {
        this.personaje = new Ranger();
        this.personaje.SetVida(VidaPoner);
        personaje.SetAtaque(ataque);

        GetComponent<BTRanger>().GetBT().Active = false;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void Inspirar() {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                int xAux = this.personaje.GetX() + i;
                int yAux = this.personaje.GetY() + j;

                if (i == 0 && j == 0)
                {
                    continue;
                }
                else if (xAux >= 0 && xAux < GameManager.GetGrid().GetGrid().GetLength(0) &&
                    yAux >= 0 && yAux < GameManager.GetGrid().GetGrid().GetLength(1) &&
                    GameManager.GetGrid().GetGrid()[xAux, yAux].isOccupied() &&
                    GameManager.GetGrid().GetGrid()[xAux, yAux].GetPersonaje().CompareTag(this.personaje.GetFaccion()))
                {
                    var aliado = GameManager.GetGrid().GetGrid()[xAux, yAux].GetPersonaje();
                    aliado.GetPersonaje().SetInspirado(true);
                    this.especialCargado = false;
                    this.cargarEspecial();
                }
            }
        }
    }
}
