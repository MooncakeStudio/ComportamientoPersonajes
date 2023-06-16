using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : Personaje
{
    private int VidaCurar;
    public Mago() : base() { }
    public Mago(int vida, int ataque, string faccion, int VidaCurar) : base(vida, ataque, faccion) { this.VidaCurar = VidaCurar; }
    public Mago(int vida, int ataque, string faccion, int VidaCurar, int x, int y) : base(vida, ataque, faccion, x, y) { this.VidaCurar = VidaCurar; }

    public int GetCuracion() { return this.VidaCurar; }
    public void SetCuracion(int VidaCurar) { this.VidaCurar = VidaCurar; }

    public override void Atacar(PersonajeController enemigo)
    {
        var enemigoAux = enemigo.GetPersonaje();
        enemigoAux.Herida(ataque);

    }

    public override PersonajeController EnemigoARango()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
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

    public PersonajeController AliadoARango()
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
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
                    var aliado = GameManager.GetGrid().GetGrid()[xAux, yAux].GetPersonaje();
                    return aliado;
                }
            }
        }

        return null;
    }
}
