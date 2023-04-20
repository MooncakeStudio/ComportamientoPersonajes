using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    // ATRIBUTOS

    [SerializeField] Grid grid;


    // GETTERS & SETTERS




    // METODOS

    public void EncuentraCamino(Vector3 posicion, Vector3 objetivo)
    {
        Celda celdaIni = grid.GetCelda(posicion);
        Celda celdaFin = grid.GetCelda(objetivo);

        List<Celda> listaAbierta = new List<Celda>();
        HashSet<Celda> listaCerrada = new HashSet<Celda>();

        listaAbierta.Add(celdaIni);

        while (listaAbierta.Count > 0)
        {
            Celda celdaActual = listaAbierta[0];

            for (int i = 1; i < listaAbierta.Count; i++)
            {
                if (listaAbierta[i].costeF < celdaActual.costeF || listaAbierta[i].costeF == celdaActual.costeF && listaAbierta[i].costeH < celdaActual.costeH)
                {
                    celdaActual = listaAbierta[i];
                }
            }

            listaAbierta.Remove(celdaActual);
            listaCerrada.Add(celdaActual);

            if(celdaActual.xGrid == celdaFin.xGrid && celdaActual.yGrid == celdaFin.yGrid)
            {
                return;
            }

            foreach (Celda vecino in grid.GetVecinos(celdaActual))
            {
                if ((!vecino.transitable && !(vecino.xGrid == celdaFin.xGrid && vecino.yGrid == celdaFin.yGrid)) || listaCerrada.Contains(vecino))
                {
                    continue;
                }

                int costeMovimiento = celdaActual.costeG + 1;
                if (costeMovimiento < vecino.costeG || !listaAbierta.Contains(vecino))
                {
                    vecino.costeG = costeMovimiento;
                    vecino.costeH = DistManhattan(vecino, celdaFin);

                    vecino.padre = celdaActual;

                    if (!listaAbierta.Contains(vecino))
                    {
                        listaAbierta.Add(vecino);
                    }
                }
            }
        }
    }

    public List<Celda> ObtenerCamino(Vector3 posIni, Vector3 posFin)
    {
        Celda ini = grid.GetCelda(posIni);
        Celda fin = grid.GetCelda(posFin);

        List<Celda> camino = new List<Celda>();
        Celda celdaActual = fin;

        while(celdaActual != ini)
        {
            camino.Add(celdaActual);
            celdaActual = celdaActual.padre;
        }

        return camino;
    }

    int DistManhattan(Celda ini, Celda fin)
    {
        int dist = Mathf.Abs(fin.xGrid - ini.xGrid) + Mathf.Abs(fin.yGrid - ini.yGrid);

        return dist;
    }
}