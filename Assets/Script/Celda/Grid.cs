using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    #region Atributos
    Celda[,] grid;

    [Header("Dimensiones")]
    [SerializeField] int alto = 25;
    [SerializeField] int ancho = 25;

    [SerializeField] float tamanyoCelda = 1f;

    [Header("Máscara de Obstáculos")]
    [SerializeField] LayerMask mascaraObstaculos;
    #endregion

    #region Getters - Setters
    public int GetAncho() { return this.ancho; }
    public int GetAlto() { return this.alto; }

    public float GetTamCelda() { return tamanyoCelda; }
    public Celda[,] GetGrid() { return this.grid; }

    #endregion

    #region Metodos

    void Awake()
    {
        this.grid = new Celda[alto, ancho];
        CompruebaTransitable();
    }
    public void CompruebaTransitable()
    {
        for (int i = 0; i < this.alto; i++)
        {
            for (int j = 0; j < this.ancho; j++)
            {
                Vector3 posicion = GetPosicionGlobal(i, j);
                bool transitable = !Physics.CheckBox
                    (posicion,
                    Vector3.one / 2 * this.tamanyoCelda, //tamano
                    Quaternion.identity, //rotacion
                    mascaraObstaculos); //capa de los colliders

                if (i == 0 || i == this.alto - 1 || j == 0 || j == this.ancho - 1)
                {
                    transitable = false;
                }

                grid[i, j] = new Celda(i, j);
                grid[i, j].transitable = transitable;
            }
        }
    }

    public Vector3 GetPosicionGlobal(int x, int z)
    {
        return new Vector3
            (transform.position.x + x * this.tamanyoCelda,
            0f,
            transform.position.z + z * this.tamanyoCelda);

    }



    public Celda GetCelda(Vector3 posicion)
    {
        float porcentajeX = (posicion.x + ancho / 2) / ancho;
        float porcentajeZ = (posicion.z + alto / 2) / alto;

        porcentajeX = Mathf.Clamp01(porcentajeX);
        porcentajeZ = Mathf.Clamp01(porcentajeZ);

        var x = Mathf.RoundToInt(porcentajeX * (grid.GetLength(0) - 1));
        var z = Mathf.RoundToInt(porcentajeZ * (grid.GetLength(1) - 1));

        return grid[x, z];
    }

    public List<Celda> GetVecinos(Celda celda)
    {
        List<Celda> vecinos = new List<Celda>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0 || i != 0 && j != 0) //Celda y sus diagonales?
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

        for (int i = 0; i < this.alto; i++)
        {
            for (int j = 0; j < this.ancho; j++)
            {
                Vector3 posicion = new Vector3
                    (transform.position.x + i * this.tamanyoCelda,
                    0f,
                    transform.position.z + j * this.tamanyoCelda);

                if (grid[i, j].transitable) { Gizmos.color = Color.green; }
                else { Gizmos.color = Color.red; }

                Gizmos.DrawCube(posicion, Vector3.one / 4);
            }
        }
    }
    #endregion
}
