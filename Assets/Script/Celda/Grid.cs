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

    // Start is called before the first frame update
    void Start()
    {
        this.grid = new Celda[alto, ancho];

        CompruebaTransitable();
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

                grid[i, j] = new Celda();
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
