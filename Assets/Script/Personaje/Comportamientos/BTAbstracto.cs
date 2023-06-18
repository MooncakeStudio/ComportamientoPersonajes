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

    public State ActiveArbol;

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

    public IEnumerator muestraBocadillo(bool accion, string texto)
    {
        GetComponent<PersonajeController>().BocadilloOn(accion, texto);
        yield return new WaitForSeconds(0.7f);
        GetComponent<PersonajeController>().BocadilloOff();
    }
}
