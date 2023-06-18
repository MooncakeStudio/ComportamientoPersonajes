using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Personaje
{
    public Ranger() : base() { }
    public Ranger(int vida, int ataque, string faccion) : base(vida, ataque, faccion) { }
    public Ranger(int vida, int ataque, string faccion, int x, int y) : base(vida, ataque, faccion, x, y) { }

    public override PersonajeController EnemigoARango()
    {
        for (int i = -5; i <= 5; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                int xAux = x + i;
                int yAux = y + j;

                if (i == 0 && j == 0)
                {
                    continue;
                }
                else if (xAux >= 0 && xAux < GameManager.GetGrid().GetGrid().GetLength(0) &&
                    yAux >= 0 && yAux < GameManager.GetGrid().GetGrid().GetLength(1) &&
                    GameManager.GetGrid().GetGrid()[xAux, yAux].isOccupied() &&
                    !GameManager.GetGrid().GetGrid()[xAux, yAux].GetPersonaje().CompareTag(faccion))
                {
                    var enemigo = GameManager.GetGrid().GetGrid()[xAux, yAux].GetPersonaje();
                    return enemigo;
                }
            }
        }

        return null;
    }

    public bool AliadoARango()
    {
        for (int i = -3; i <= 3; i++)
        {
            for (int j = -3; j <= 3; j++)
            {
                int xAux = x + i;
                int yAux = y + j;

                if (i == 0 && j == 0)
                {
                    continue;
                }
                else if (xAux >= 0 && xAux < GameManager.GetGrid().GetGrid().GetLength(0) &&
                    yAux >= 0 && yAux < GameManager.GetGrid().GetGrid().GetLength(1) &&
                    GameManager.GetGrid().GetGrid()[xAux, yAux].isOccupied() &&
                    GameManager.GetGrid().GetGrid()[xAux, yAux].GetPersonaje().CompareTag(faccion))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
