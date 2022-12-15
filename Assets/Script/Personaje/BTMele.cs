using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using System;

public class BTMele : MonoBehaviour
{

    [SerializeField] Sprite spriteDrcha;
    [SerializeField] Sprite spriteIzqda;

    private BehaviourTreeEngine BTPersonaje;
    public bool esTurno = true;

    public Vector3 objetivo;

    public PathFinding pf;

    public void Start()
    {
        BTPersonaje = new BehaviourTreeEngine(false);


        CrearIA();
    }

    private void CrearIA()
    {
        // Nodos
        SelectorNode nodoSeleccion = BTPersonaje.CreateSelectorNode("selectorNode");
        SequenceNode nodoSecuencia = BTPersonaje.CreateSequenceNode("seqNode", false);
        LeafNode compruebaObjetivo = BTPersonaje.CreateLeafNode("compruebaObjetivo", compruebaObjetivoAction, compruebaObjetivoSuccessCheck);
        LeafNode irObjetivo = BTPersonaje.CreateLeafNode("irObjetivo", irObjetivoAction, irObjetivoSuccessCheck);
        LeafNode idle = BTPersonaje.CreateLeafNode("idle", idleAction, idleSuccessCheck);

        LoopDecoratorNode mainLoop = BTPersonaje.CreateLoopNode("loop", nodoSeleccion);

        // Añadir hijos
        nodoSeleccion.AddChild(nodoSecuencia);
        nodoSeleccion.AddChild(idle);

        nodoSecuencia.AddChild(compruebaObjetivo);
        nodoSecuencia.AddChild(irObjetivo);

        // Establecer Raíz
        BTPersonaje.SetRootNode(mainLoop);
    }

    public void Update()
    {
        if (esTurno)
        {
            BTPersonaje.Update();
        }
    }

    private void compruebaObjetivoAction()
    {
        if (GameManager.hayObj == true)
        {
            objetivo = GameManager.objetivo;
        }
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
        Moverse(objetivo);
    }

    private ReturnValues irObjetivoSuccessCheck()
    {
        return ReturnValues.Succeed;
    }

    private void idleAction()
    {

    }

    private ReturnValues idleSuccessCheck()
    {
        return ReturnValues.Succeed;
    }

    public void Moverse(Vector3 objetivo)
    {
        pf.EncuentraCamino(transform.position, objetivo);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Vida(Clone)")
        {
            Destroy(collision.gameObject);
        }
    }
}
