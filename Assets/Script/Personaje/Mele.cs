using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele : Personaje
{
    public void Atacar(PersonajeController enemigo)
    {
        var enemigoAux = enemigo.GetPersonaje();

        enemigoAux.SetVida(enemigoAux.GetVida() - this.ataque);
    }

    public void EnemigoARango()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int xAux = this.x + i;
                int yAux = this.y + j;

                if (xAux >= 0 && xAux < GameManager.GetGrid().GetGrid().GetLength(0) && yAux >= 0 && yAux < GameManager.GetGrid().GetGrid().GetLength(1) && GameManager.GetGrid().GetGrid()[xAux, yAux].isOccupied())
                {
                    var enemigo = GameManager.GetGrid().GetGrid()[xAux, yAux].GetPersonaje();
                    return enemigo;
                }
            }
        }
    }
}
