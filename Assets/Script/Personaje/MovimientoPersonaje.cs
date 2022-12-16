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

    public int GetX() { return x; }
    public int GetY() { return y; }

    public void SetX(int x) { this.x = x; }
    public void SetY(int y) { this.y = y; }


    // METODOS

    private void Awake()
    {
        arbol = GetComponent<BTMele>();
    }

    private void Start()
    {
        grid = GameManager.GetGrid();

        //do
        //{
        //    x = Random.Range(0, this.grid.GetGrid().GetLength(0));
        //    y = Random.Range(0, this.grid.GetGrid().GetLength(1));
        //} while (!grid.GetGrid()[x, y].transitable);

        GetComponent<PersonajeController>().GetPersonaje().SetX(x);
        GetComponent<PersonajeController>().GetPersonaje().SetY(y);

        grid.GetGrid()[x, y].SetPersonaje(GetComponent<PersonajeController>());

        transform.position = grid.GetPosicionGlobal(this.x, this.y);

    }

    private void FixedUpdate()
    {
        //arbol.GetBT().Update();

        if(objet != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, grid.GetPosicionGlobal(objet.xGrid, objet.yGrid), 1 * Time.deltaTime);

            if (Vector3.Distance(transform.position, grid.GetPosicionGlobal(objet.xGrid, objet.yGrid)) < 0.05f)
            {
                grid.GetGrid()[x, y].SetPersonaje(null);

                x = objet.xGrid;
                y = objet.yGrid;

                Debug.Log(GetComponent<PersonajeController>().GetPersonaje().GetX());
                Debug.Log(GetComponent<PersonajeController>().GetPersonaje().GetY());

                GetComponent<PersonajeController>().GetPersonaje().SetX(x);
                GetComponent<PersonajeController>().GetPersonaje().SetY(y);

                Debug.Log(GetComponent<PersonajeController>().GetPersonaje().GetX());
                Debug.Log(GetComponent<PersonajeController>().GetPersonaje().GetY());

                grid.GetGrid()[x, y].SetPersonaje(GetComponent<PersonajeController>());

                objet = null;
            }
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
            var personaje = GetComponent<PersonajeController>().GetPersonaje();

            personaje.SetVida(personaje.GetVida() + 5);

            GetComponent<PersonajeController>().SetPersonaje(personaje);

            Debug.Log("Mira mi vida: " + personaje.GetVida());

            Destroy(collision.gameObject);
        }
    }
}
