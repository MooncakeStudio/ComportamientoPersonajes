using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mele : Personaje
{  

    public override void Atacar(PersonajeController enemigo)
    {
        var enemigoAux = enemigo.GetPersonaje();

        enemigoAux.SetVida(enemigoAux.GetVida() - this.ataque);

        Debug.Log("Le he dejao a : " + enemigoAux.GetVida());
    }

    public override PersonajeController EnemigoARango()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int xAux = this.x + i;
                int yAux = this.y + j;

                if(i == 0 && j == 0)
                {
                    continue;
                }
                else if (xAux >= 0 && xAux < GameManager.GetGrid().GetGrid().GetLength(0) && yAux >= 0 && yAux < GameManager.GetGrid().GetGrid().GetLength(1) && GameManager.GetGrid().GetGrid()[xAux, yAux].isOccupied())
                {
                    var enemigo = GameManager.GetGrid().GetGrid()[xAux, yAux].GetPersonaje();
                    return enemigo;
                }
            }
        }

        return null;
    }
}
