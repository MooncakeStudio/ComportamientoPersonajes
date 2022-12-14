using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class movimiento : MonoBehaviour
{

    [SerializeField] Sprite spriteDrcha;
    [SerializeField] Sprite spriteIzqda;

    private BehaviourTreeEngine BTPersonaje;
    public bool esTurno = false;

    public Vector3 objetivo;

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

        // A�adir hijos
        nodoSeleccion.AddChild(nodoSecuencia);
        nodoSeleccion.AddChild(idle);

        nodoSecuencia.AddChild(compruebaObjetivo);
        nodoSecuencia.AddChild(irObjetivo);

        // Establecer Ra�z
        BTPersonaje.SetRootNode(mainLoop);
    }

    public void realizaAccion()
    {
        BTPersonaje.Update();
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

        Vector3 u_distancia = (objetivo - gameObject.transform.position).normalized;

        gameObject.transform.position += u_distancia * 5;

        esTurno = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
