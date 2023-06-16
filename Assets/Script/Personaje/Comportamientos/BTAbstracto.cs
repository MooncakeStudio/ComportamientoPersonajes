using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAbstracto : MonoBehaviour
{
    
    #region Atributos, getters-setters y constructor
    protected BehaviourTreeEngine BT = new BehaviourTreeEngine();

    Grid grid;

    public Vector3 objetivo;
    protected PersonajeController enemigo;

    public BTAbstracto() 
    { 
        this.grid = GameManager.GetGrid();
    }

    public BehaviourTreeEngine GetBT() { return this.BT; }
    #endregion

    
    #region Metodos
    virtual public void CrearIA(){}

    #region Acciones

    virtual public IEnumerator ejecutarArbol()
    {
        yield return null;
    }

    virtual public void IniciaArbol()
    {
        StartCoroutine(ejecutarArbol());
    }
    #endregion

    #endregion
}
