using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [Header("Posición")]
    [SerializeField] int x;
    [SerializeField] int y;

    [Header("Tablero")]
    [SerializeField] Grid grid;

    BTMele arbol;

    private void Awake()
    {
        arbol = GetComponent<BTMele>();
    }

    private void Start()
    {
        do
        {
            x = Random.Range(0, this.grid.GetGrid().GetLength(0));
            y = Random.Range(0, this.grid.GetGrid().GetLength(1));
        } while (!grid.GetGrid()[x, y].transitable);

        transform.position = grid.GetPosicionGlobal(this.x, this.y);

    }

    private void Update()
    {
        arbol.Update();
    }

    public IEnumerator Moverse()
    {
        int eleccion = Random.Range(0, 4);

        switch (eleccion)
        {
            case 0:
                if (y+1 < this.grid.GetGrid().GetLength(1) && grid.GetGrid()[x, y + 1].transitable)
                {
                    MoverArriba();
                }
                else
                {
                    break;
                }
                break;
            case 1:
                if (x - 1 < this.grid.GetGrid().GetLength(1) && grid.GetGrid()[x - 1,y].transitable)
                {
                    MoverIzquierda();
                }
                else
                {
                    break;
                }
                break;
            case 2:
                if (y - 1 < this.grid.GetGrid().GetLength(1) && grid.GetGrid()[x, y - 1].transitable)
                {
                    MoverAbajo();
                }
                else
                {
                    break;
                }
                break;
            case 3:
                if (x + 1 < this.grid.GetGrid().GetLength(1) && grid.GetGrid()[x + 1, y].transitable)
                {
                    MoverDerecha();
                }
                else
                {
                    break;
                }
                break;
            default:
                break;
        }

        //yield return new WaitForSeconds(2);
        yield return null;

        StartCoroutine(Moverse());
    }

    public void MoverArriba() 
    {
        this.y += 1;

        transform.position = grid.GetPosicionGlobal(this.x, this.y);
    }

    public void MoverIzquierda() 
    {
        this.x -= 1;

        transform.position = grid.GetPosicionGlobal(this.x, this.y);
    }

    public void MoverAbajo() 
    {
        this.y -= 1;

        transform.position = grid.GetPosicionGlobal(this.x, this.y);
    }

    public void MoverDerecha() 
    {
        this.x += 1;

        transform.position = grid.GetPosicionGlobal(this.x, this.y);
    }
}
