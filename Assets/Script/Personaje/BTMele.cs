using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using System;

public class BTMele : MonoBehaviour
{
    // ATRIBUTOS

    [SerializeField] Sprite spriteDrcha;
    [SerializeField] Sprite spriteIzqda;

    private BehaviourTreeEngine BT;

    [SerializeField] Grid grid;
    public Vector3 objetivo;


    // GETTERS & SETTERS

    public BehaviourTreeEngine GetBT() { return this.BT; }


    // METODOS

    public void Start()
    {
        BT = new BehaviourTreeEngine(false);

        CrearIA();

        StartCoroutine(ejecutarArbol());
    }

    private void CrearIA()
    {
        // Nodos
        SelectorNode nodoSeleccion = BT.CreateSelectorNode("selectorNode");
        SequenceNode nodoSecuencia = BT.CreateSequenceNode("seqNode", false);
        LeafNode compruebaObjetivo = BT.CreateLeafNode("compruebaObjetivo", compruebaObjetivoAction, compruebaObjetivoSuccessCheck);
        LeafNode irObjetivo = BT.CreateLeafNode("irObjetivo", irObjetivoAction, irObjetivoSuccessCheck);
        LeafNode idle = BT.CreateLeafNode("idle", idleAction, idleSuccessCheck);

        LoopDecoratorNode mainLoop = BT.CreateLoopNode("loop", nodoSeleccion);

        // Añadir hijos
        nodoSeleccion.AddChild(nodoSecuencia);
        nodoSeleccion.AddChild(idle);

        nodoSecuencia.AddChild(compruebaObjetivo);
        nodoSecuencia.AddChild(irObjetivo);

        // Establecer Raíz
        BT.SetRootNode(mainLoop);
    }

    /* ACCIONES */

    private void compruebaObjetivoAction() {}

    IEnumerator ejecutarArbol()
    {
        BT.Update();
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.25f));
        StartCoroutine(ejecutarArbol());
    }

    private ReturnValues compruebaObjetivoSuccessCheck()
    {
        if (GameManager.hayObj == true)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void irObjetivoAction()
    {
        var movimientoManager = GetComponent<MovimientoPersonaje>();

        movimientoManager.Moverse(GameManager.objetivo);
    }

    private ReturnValues irObjetivoSuccessCheck()
    {
        return ReturnValues.Succeed;
    }

    private void idleAction() {}

    private ReturnValues idleSuccessCheck()
    {
        return ReturnValues.Succeed;
    }
}
