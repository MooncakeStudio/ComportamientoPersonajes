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

    Celda objet;

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
        //arbol.GetBT().Update();

        if(objet != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, grid.GetPosicionGlobal(camino[0].xGrid, camino[0].yGrid), 1 * Time.deltaTime);

            if (Vector3.Distance(transform.position, grid.GetPosicionGlobal(camino[0].xGrid, camino[0].yGrid)) < 0.05f)
                objet = null;
        }
    }

    public void Moverse(Vector3 objetivo)
    {
        pf.EncuentraCamino(transform.position, objetivo);
        this.camino = pf.ObtenerCamino(transform.position, objetivo);

        if(camino.Count > 0)
        {
            camino.Reverse();

            objet = camino[0];

            
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Objeto"))
        {
            Destroy(collision.gameObject);
        }
    }
}
