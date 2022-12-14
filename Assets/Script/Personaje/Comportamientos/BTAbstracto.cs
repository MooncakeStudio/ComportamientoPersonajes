using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAbstracto : MonoBehaviour
{
    // ATRIBUTOS

    protected BehaviourTreeEngine BT = new BehaviourTreeEngine();

    Grid grid;

    public Vector3 objetivo;
    protected PersonajeController enemigo;


    public BTAbstracto() 
    { 
        this.grid = GameManager.GetGrid();
    }

    // GETTERS & SETTERS

    public BehaviourTreeEngine GetBT() { return this.BT; }


    // METODOS

    virtual public void CrearIA(){}

    /* ACCIONES */

    virtual public IEnumerator ejecutarArbol()
    {
        yield return null;
    }

    virtual public void IniciaArbol()
    {
        StartCoroutine(ejecutarArbol());
    }
}
