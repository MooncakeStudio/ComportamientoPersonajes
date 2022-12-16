using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    // ATRIBUTOS

    [Header("Posición")]
    [SerializeField] int x;
    [SerializeField] int y;

    [Header("Tablero")]
    Grid grid;

    [SerializeField] PathFinding pf;
    BTMele arbol;
    List<Celda> camino;


    // GETTERS & SETTERS




    // METODOS

    private void Awake()
    {
        arbol = GetComponent<BTMele>();
    }

    private void Start()
    {
        grid = GameManager.GetGrid();

        do
        {
            x = Random.Range(0, this.grid.GetGrid().GetLength(0));
            y = Random.Range(0, this.grid.GetGrid().GetLength(1));
        } while (!grid.GetGrid()[x, y].transitable);

        transform.position = grid.GetPosicionGlobal(this.x, this.y);

    }

    private void Update()
    {
        arbol.GetBT().Update();
    }

    public void Moverse(Vector3 objetivo)
    {
        pf.EncuentraCamino(transform.position, objetivo);
        this.camino = pf.ObtenerCamino(transform.position, objetivo);

        camino.Reverse();

        transform.position = grid.GetPosicionGlobal(camino[0].xGrid, camino[0].yGrid);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Objeto"))
        {
            Destroy(collision.gameObject);
        }
    }
}
