using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    #region Atributos
    [Header("Posición")]
    [SerializeField] int x;
    [SerializeField] int y;

    [Header("Tablero")]
    BTMele arbol;
    List<Celda> camino;

    Celda objet;

    #endregion

    #region Getters-Setters
    public int GetX() { return x; }
    public int GetY() { return y; }

    public void SetX(int x) { this.x = x; }
    public void SetY(int y) { this.y = y; }
    #endregion

    #region Metodos
    private void Awake() { arbol = GetComponent<BTMele>(); }

    private void Start()
    {
        //do
        //{
        //    x = Random.Range(0, this.grid.GetGrid().GetLength(0));
        //    y = Random.Range(0, this.grid.GetGrid().GetLength(1));
        //} while (!grid.GetGrid()[x, y].transitable);

        GetComponent<PersonajeController>().GetPersonaje().SetX(x);
        GetComponent<PersonajeController>().GetPersonaje().SetY(y);

        GameManager.grid.GetGrid()[x, y].SetPersonaje(GetComponent<PersonajeController>());

        transform.position = GameManager.grid.GetPosicionGlobal(this.x, this.y);

    }

    private void FixedUpdate()
    {
        //arbol.GetBT().Update();

        if (objet != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.grid.GetPosicionGlobal(objet.xGrid, objet.yGrid), 1 * Time.deltaTime);

            if (Vector3.Distance(transform.position, GameManager.grid.GetPosicionGlobal(objet.xGrid, objet.yGrid)) < 0.05f)
            {
                GameManager.grid.GetGrid()[x, y].SetPersonaje(null);

                x = objet.xGrid;
                y = objet.yGrid;

                GetComponent<PersonajeController>().GetPersonaje().SetX(x);
                GetComponent<PersonajeController>().GetPersonaje().SetY(y);

                GameManager.grid.GetGrid()[x, y].SetPersonaje(GetComponent<PersonajeController>());

                objet = null;
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Objeto"))
    //    {
    //        var personaje = GetComponent<PersonajeController>().GetPersonaje();

    //        personaje.SetVida(personaje.GetVida() + 5);

    //        GetComponent<PersonajeController>().SetPersonaje(personaje);

    //        if(personaje.GetVida() > 20)
    //        {
    //            GetComponent<PersonajeController>().noMasAuxilio();
    //        }
    //        Debug.Log("Mira mi vida: " + personaje.GetVida());

    //        Destroy(collision.gameObject);
    //    }
    //}
    #endregion
}
