using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using System;

public class BTMele : BTAbstracto
{
    public BTMele() : base()
    {
        this.BT = new BehaviourTreeEngine(false);

        this.CrearIA();
    }


    // METODOS

    private void Start()
    {
        StartCoroutine(ejecutarArbol());
    }

    public override void CrearIA()
    {
        // Nodos
        SelectorNode nodoSelec = BT.CreateSelectorNode("nodoSelec");
        SequenceNode nodoSec = BT.CreateSequenceNode("nodoSec", false);
        LeafNode EnemigoARango = BT.CreateLeafNode("EnemigoARango", EnemigoARangoAction, EnemigoARangoSuccessCheck);
        SelectorNode nodoSelec2 = BT.CreateSelectorNode("nodoSelec2");
        SequenceNode nodoSec2 = BT.CreateSequenceNode("nodoSec2", false);
        LeafNode SuficienteVida = BT.CreateLeafNode("SuficienteVida", SuficienteVidaAction, SuficienteVidaSuccessCheck);
        LeafNode Atacar = BT.CreateLeafNode("Atacar", AtacarAction, AtacarSuccessCheck);
        SequenceNode nodoSec3 = BT.CreateSequenceNode("nodoSec3", false);
        LeafNode EnemigoPocaVida = BT.CreateLeafNode("EnemigoPocaVida", EnemigoPocaVidaAction, EnemigoPocaVidaSuccessCheck);
        LeafNode Atacar2 = BT.CreateLeafNode("Atacar2", AtacarAction, AtacarSuccessCheck);
        SequenceNode nodoSec4 = BT.CreateSequenceNode("nodoSec4", false);
        LeafNode PocaVida = BT.CreateLeafNode("PocaVida", PocaVidaAction, PocaVidaSuccessCheck);
        LeafNode VidaGenerada = BT.CreateLeafNode("VidaGenerada", VidaGeneradaAction, VidaGeneradaSuccessCheck);
        LeafNode IrAVida = BT.CreateLeafNode("IrAVida", IrAVidaAction, IrAVidaSuccessCheck);


        LoopDecoratorNode mainLoop = BT.CreateLoopNode("loop", nodoSelec);

        // Añadir hijos
        nodoSelec.AddChild(nodoSec);
        nodoSelec.AddChild(nodoSec4);

        nodoSec.AddChild(EnemigoARango);
        nodoSec.AddChild(nodoSelec2);

        nodoSelec2.AddChild(nodoSec2);
        nodoSelec2.AddChild(nodoSec3);

        nodoSec2.AddChild(SuficienteVida);
        nodoSec2.AddChild(Atacar);

        nodoSec3.AddChild(EnemigoPocaVida);
        nodoSec3.AddChild(Atacar2);

        nodoSec4.AddChild(PocaVida);
        nodoSec4.AddChild(VidaGenerada);
        nodoSec4.AddChild(IrAVida);

        // Establecer Raíz
        BT.SetRootNode(mainLoop);
    }

    /* ACCIONES */

    private void EnemigoARangoAction() { }

    private ReturnValues EnemigoARangoSuccessCheck()
    {
        enemigo = GetComponent<PersonajeController>().GetPersonaje().EnemigoARango();

        if(enemigo != null)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void SuficienteVidaAction() { }

    private ReturnValues SuficienteVidaSuccessCheck()
    {
        if (GetComponent<PersonajeController>().GetPersonaje().GetVida() > 20)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void AtacarAction()
    {
        Debug.Log("Lo agarro a putasos");
        GetComponent<PersonajeController>().GetPersonaje().Atacar(enemigo);
    }

    private ReturnValues AtacarSuccessCheck()
    {
        return ReturnValues.Succeed;
    }

    private void EnemigoPocaVidaAction() { }

    private ReturnValues EnemigoPocaVidaSuccessCheck()
    {
        if (enemigo.GetComponent<PersonajeController>().GetPersonaje().GetVida() <= 15)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void PocaVidaAction() { }

    private ReturnValues PocaVidaSuccessCheck()
    {
        if (GetComponent<PersonajeController>().GetPersonaje().GetVida() <= 20)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void VidaGeneradaAction() { }

    private ReturnValues VidaGeneradaSuccessCheck()
    {
        if (FindObjectOfType<ObjectController>())
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void IrAVidaAction()
    {
        var movimientoManager = GetComponent<MovimientoPersonaje>();

        movimientoManager.Moverse(GameManager.objetivo);
    }

    private ReturnValues IrAVidaSuccessCheck()
    {
        return ReturnValues.Succeed;
    }
}
