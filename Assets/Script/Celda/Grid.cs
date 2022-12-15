using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Celda[,] grid;

    [Header("Dimensiones")]
    [SerializeField] int alto = 25;
    [SerializeField] int ancho = 25;

    [SerializeField]float tamanyoCelda = 1f;

    [Header("Máscara de Obstáculos")]
    [SerializeField] LayerMask mascaraObstaculos;

    public List<Celda> camino;

    public int GetAncho() { return this.ancho; }
    public int GetAlto() { return this.alto; }
    public Celda[,] GetGrid() { return this.grid; }

    // Start is called before the first frame update
    void Awake()
    {
        this.grid = new Celda[alto, ancho];

        CompruebaTransitable();
    }


    public List<Celda> GetVecinos(Celda celda)
    {
        List<Celda> vecinos = new List<Celda>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0 || i != 0 && j != 0)
                {
                    continue;
                }

                int xAux = celda.xGrid + i;
                int yAux = celda.yGrid + j;

                if (xAux >= 0 && xAux < grid.GetLength(0) && yAux >= 0 && yAux < grid.GetLength(1))
                {
                    vecinos.Add(grid[xAux, yAux]);
                }
            }
        }

        return vecinos;
    }

    public void OnDrawGizmos()
    {
        if (grid == null) { return; }

        for(int i = 0; i < this.alto; i++)
        {
            for(int j = 0; j < this.ancho; j++)
            {
                Vector3 posicion = new Vector3(transform.position.x + i * this.tamanyoCelda, 0f, transform.position.z + j * this.tamanyoCelda);

                if (grid[i, j].transitable)
                {
                    Gizmos.color = Color.green;
                } else
                {
                    Gizmos.color = Color.red;
                }

                if(camino != null)
                {
                    if (camino.Contains(grid[i, j]))
                    {
                        Gizmos.color = Color.white;
                    }
                }

                Gizmos.DrawCube(posicion, Vector3.one/4);
            }
        }
    }

    public void CompruebaTransitable()
    {
        for (int i = 0; i < this.alto; i++)
        {
            for (int j = 0; j < this.ancho; j++)
            {
                Vector3 posicion = GetPosicionGlobal(i, j);
                
                bool transitable = !Physics.CheckBox(posicion, Vector3.one / 2 * this.tamanyoCelda, Quaternion.identity, mascaraObstaculos);

                grid[i, j] = new Celda(i,j);
                grid[i, j].transitable = transitable;
            }
        }
    }

    public Vector3 GetPosicionGlobal(int x, int z)
    {
        Vector3 posicion = new Vector3(transform.position.x + x * this.tamanyoCelda, 0f, transform.position.z + z * this.tamanyoCelda);

        return posicion;
    }
}
