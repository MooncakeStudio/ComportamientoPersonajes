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
    [SerializeField] Grid grid;

    public Transform buscador;

    Vector3 objvo;

    private void Update()
    {
        //EncuentraCamino(buscador.position, objvo);
    }

    public void EncuentraCamino(Vector3 posicion, Vector3 objetivo)
    {
        Celda celdaIni = CeldaDeGlobal(posicion);
        Celda celdaFin = CeldaDeGlobal(objetivo);

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
                ObtenerCamino(celdaIni, celdaFin);
                return;
            }

            foreach (Celda vecino in grid.GetVecinos(celdaActual))
            {
                if (!vecino.transitable || listaCerrada.Contains(vecino))
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

    void ObtenerCamino(Celda ini, Celda fin)
    {
        List<Celda> camino = new List<Celda>();
        Celda celdaActual = fin;

        while(celdaActual != ini)
        {
            camino.Add(celdaActual);
            celdaActual = celdaActual.padre;
        }

        camino.Reverse();

        grid.camino = camino;

    }

    Celda CeldaDeGlobal(Vector3 posicion)
    {
        float porcentajeX = (posicion.x + grid.GetAncho()/2)/grid.GetAncho();
        float porcentajeZ = (posicion.z + grid.GetAlto()/2)/grid.GetAlto();

        //float porcentajeX = (posicion.x) / grid.GetAncho();
        //float porcentajeZ = (posicion.z) / grid.GetAlto();

        porcentajeX = Mathf.Clamp01(porcentajeX);
        porcentajeZ = Mathf.Clamp01(porcentajeZ);

        var x = Mathf.RoundToInt(porcentajeX * (grid.GetGrid().GetLength(0) - 1));
        var z = Mathf.RoundToInt(porcentajeZ * (grid.GetGrid().GetLength(1) - 1));

        return grid.GetGrid()[x, z];
    }

    int DistManhattan(Celda ini, Celda fin)
    {
        int dist = Mathf.Abs(fin.xGrid - ini.xGrid) + Mathf.Abs(fin.yGrid - ini.yGrid);

        return dist;
    }
}