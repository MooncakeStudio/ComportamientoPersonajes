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


    public override IEnumerator ejecutarArbol()
    {
        BT.Update();

        yield return new WaitForSeconds(GetComponent<PersonajeController>().GetVelocidad());

        //BT.Reset();
        StartCoroutine(ejecutarArbol());
    }

    public override void CrearIA()
    {
        //Nodo root
        SelectorNode nodoRoot = BT.CreateSelectorNode("RootSelector");

        //Primera fila nodos
        SequenceNode provocandoSecuencia = BT.CreateSequenceNode("ProvocandoSec", false);
        SequenceNode provocarSecuencia = BT.CreateSequenceNode("ProvocarSec", false);
        SequenceNode ataqueSecuencia = BT.CreateSequenceNode("AtaqueSec", false);
        SelectorNode moverseSelector = BT.CreateSelectorNode("MoverseSelec");

        //Nodos provocando
        LeafNode enemigoProvocandoPerception = BT.CreateLeafNode("EnemigoProvocando", EnemigoProvocandoAction, EnemigoProvocandoSuccessCheck);
        SelectorNode decisionProvocando = BT.CreateSelectorNode("DecisionProvocando");
        SequenceNode secuenciaProvocando = BT.CreateSequenceNode("SecuenciaProvocando", false);
        LeafNode enemigoRangoPercepcion = BT.CreateLeafNode("EnemigoRango", EnemigoARangoAction, EnemigoARangoSuccessCheck);
        LeafNode atacarAccion = BT.CreateLeafNode("Atacar", AtacarAction, AtacarSuccessCheck);
        LeafNode moverseAccion = BT.CreateLeafNode("Moverse", MoverseEnemigoAction, MoverseEnemigoSuccessCheck);

        //Nodos Provocar
        LeafNode aliadoAuxilio = BT.CreateLeafNode("AliadoAuxilio", AliadoAuxilioAction, AliadoAuxilioSuccessCheck);
        LeafNode especialCargado = BT.CreateLeafNode("EspecialCargado", EspecialCargadoAction, EspecialCargadoSuccessCheck);
        LeafNode SuficienteVida = BT.CreateLeafNode("SuficienteVida", SuficienteVidaAction, SuficienteVidaSuccessCheck);
        LeafNode provocarAccion = BT.CreateLeafNode("Provocar", ProvocarAction, ProvocarSuccessCheck);

        //Nodos Atacar
        LeafNode EnemigoARango = BT.CreateLeafNode("EnemigoARango", EnemigoARangoAction, EnemigoARangoSuccessCheck);
        SelectorNode selecAtaque1 = BT.CreateSelectorNode("SelectorAtaque1");
        SequenceNode secAtaque1 = BT.CreateSequenceNode("SecAtaque1", false);
        LeafNode suficienteVidaAtaque = BT.CreateLeafNode("VidaAataque", SuficienteVidaAction, SuficienteVidaSuccessCheck);
        LeafNode atacarAtaque = BT.CreateLeafNode("AtacarAtaque", AtacarAction, AtacarSuccessCheck);
        SelectorNode selecAtaque2 = BT.CreateSelectorNode("SelectorAtaque2");
        SequenceNode secAtaque2 = BT.CreateSequenceNode("SecAtaque2", false);
        LeafNode EnemigoPocaVida = BT.CreateLeafNode("EnemigoPocaVida", EnemigoPocaVidaAction, EnemigoPocaVidaSuccessCheck);
        LeafNode atacarEnemigo = BT.CreateLeafNode("AtacarEnemigo", AtacarAction, AtacarSuccessCheck);
        LeafNode pedirAuxilioAccion = BT.CreateLeafNode("PedirAuxilio", PedirAuxilioAction, PedirAuxilioSuccessCheck);


        //Nodos moverse
        LeafNode moverse = BT.CreateLeafNode("MoverseEnemigo", MoverseEnemigoAction, MoverseEnemigoSuccessCheck);
        SequenceNode secMoverse1 = BT.CreateSequenceNode("SecMoverse1", false);
        LeafNode PocaVida = BT.CreateLeafNode("PocaVida", PocaVidaAction, PocaVidaSuccessCheck);
        SelectorNode selecMoverse = BT.CreateSelectorNode("SelecMoverse");
        LeafNode auxilio = BT.CreateLeafNode("AuxilioPedir", PedirAuxilioAction, PedirAuxilioSuccessCheck);
        SequenceNode vidaGenerado = BT.CreateSequenceNode("VidaGeneradaSec", false);
        LeafNode VidaGenerada = BT.CreateLeafNode("VidaGenerada", VidaGeneradaAction, VidaGeneradaSuccessCheck);
        LeafNode IrAVida = BT.CreateLeafNode("IrAVida", IrAVidaAction, IrAVidaSuccessCheck);

        LoopDecoratorNode mainLoop = BT.CreateLoopNode("loop", nodoRoot);

        // Añadir hijos Provocando
        provocandoSecuencia.AddChild(enemigoProvocandoPerception);
        provocandoSecuencia.AddChild(decisionProvocando);
        decisionProvocando.AddChild(secuenciaProvocando);
        decisionProvocando.AddChild(moverseAccion);
        secuenciaProvocando.AddChild(enemigoRangoPercepcion);
        secuenciaProvocando.AddChild(atacarAccion);

        //Añadir hijos Provocar
        provocarSecuencia.AddChild(aliadoAuxilio);
        provocarSecuencia.AddChild(especialCargado);
        provocarSecuencia.AddChild(SuficienteVida);
        provocarSecuencia.AddChild(provocarAccion);

        //Añadir hijos Ataque
        ataqueSecuencia.AddChild(enemigoRangoPercepcion);
        ataqueSecuencia.AddChild(selecAtaque1);
        selecAtaque1.AddChild(secAtaque1);
        selecAtaque1.AddChild(selecAtaque2);
        secAtaque1.AddChild(suficienteVidaAtaque);
        secAtaque1.AddChild(atacarAtaque);
        selecAtaque2.AddChild(secAtaque2);
        //selecAtaque2.AddChild(pedirAuxilioAccion);
        secAtaque2.AddChild(EnemigoPocaVida);
        secAtaque2.AddChild(atacarEnemigo);

        //Añadir hijos moverse
        moverseSelector.AddChild(secMoverse1);
        moverseSelector.AddChild(moverse);
        secMoverse1.AddChild(PocaVida);
        secMoverse1.AddChild(selecMoverse);
        selecMoverse.AddChild(vidaGenerado);
        //elecMoverse.AddChild(auxilio);
        vidaGenerado.AddChild(VidaGenerada);
        vidaGenerado.AddChild(IrAVida);

        nodoRoot.AddChild(provocandoSecuencia);
        nodoRoot.AddChild(provocarSecuencia);
        nodoRoot.AddChild(ataqueSecuencia);
        nodoRoot.AddChild(moverseSelector);

        // Establecer Raíz
        BT.SetRootNode(nodoRoot);
    }

    /* ACCIONES */

    //Enemgio provocando
    private void EnemigoProvocandoAction() { }

    private ReturnValues EnemigoProvocandoSuccessCheck()
    {
        return ReturnValues.Failed;

    }

    //Moverse enemigo
    private void MoverseEnemigoAction() 
    {
        var enemigo = GetComponent<PersonajeController>().getEnemigoObjetivo();

        GetComponent<MeleeController>().Moverse(enemigo.transform.position);

        Debug.Log(gameObject.name + "Me muevo al enemigo");

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }

    private ReturnValues MoverseEnemigoSuccessCheck() 
    {
        /*enemigo = GetComponent<MeleeController>().GetPersonaje().EnemigoARango();

        if (enemigo != null)
        {
            Debug.Log(gameObject.name + "Enemigo a rango");
            return ReturnValues.Failed;
        }
        else
        {
            Debug.Log("Enemigo no a rango, me muevo");
            return ReturnValues.Succeed;
        }*/

        return ReturnValues.Succeed;
    }

    //Enemigo a rango
    private void EnemigoARangoAction() { }

    private ReturnValues EnemigoARangoSuccessCheck()
    {
        enemigo = GetComponent<MeleeController>().GetPersonaje().EnemigoARango();

        if(enemigo != null)
        {
            Debug.Log(gameObject.name +  "Enemigo a rango");
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    //Aliado auxilio
    private void AliadoAuxilioAction() { }

    private ReturnValues AliadoAuxilioSuccessCheck() 
    {
        if (GetComponent<PersonajeController>().alguienPidiendoAuxilio())
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    
    }

    //Especial cargado
    private void EspecialCargadoAction() { }

    private ReturnValues EspecialCargadoSuccessCheck()
    {
        if (GetComponent<PersonajeController>().tengoAtaqueEspecial())
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    //Suficiente vida
    private void SuficienteVidaAction() { }

    private ReturnValues SuficienteVidaSuccessCheck()
    {
        if (GetComponent<MeleeController>().GetPersonaje().GetVida() > 20)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    //Provocar
    private void ProvocarAction() 
    {
        GetComponent<MeleeController>().Provocar();

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
        //GetComponent<PersonajeController>().FinTurno();
    }

    private ReturnValues ProvocarSuccessCheck() { return ReturnValues.Succeed; }

    //Atacar
    private void AtacarAction()
    {
        Debug.Log(gameObject.name + "Lo agarro a putasos");
        GetComponent<MeleeController>().GetPersonaje().Atacar(enemigo);

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
        //GetComponent<PersonajeController>().FinTurno();
    }

    private ReturnValues AtacarSuccessCheck()
    {
        return ReturnValues.Succeed;
    }


    //enemigo poca vida
    private void EnemigoPocaVidaAction() { }

    private ReturnValues EnemigoPocaVidaSuccessCheck()
    {
        if (enemigo.GetComponent<PersonajeController>().GetPersonaje().GetVida() < 
            GetComponent<MeleeController>().GetPersonaje().GetVida())
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }


    //No suficiente vida
    private void PocaVidaAction() { }

    private ReturnValues PocaVidaSuccessCheck()
    {
        if (GetComponent<MeleeController>().GetPersonaje().GetVida() <= 20)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }


    //vida generada
    private void VidaGeneradaAction() { }

    private ReturnValues VidaGeneradaSuccessCheck()
    {
        if (GameManager.hayObj)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }


    //moverse vida
    private void IrAVidaAction()
    {
        Debug.Log("Devuelveme la vida");

        var movimientoManager = GetComponent<MeleeController>();

        movimientoManager.Moverse(GameManager.objetivo);

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }

    private ReturnValues IrAVidaSuccessCheck()
    {
        return ReturnValues.Succeed;
    }

    //Pedir auxilio
    private void PedirAuxilioAction() 
    {
        var aliado = GetComponent<PersonajeController>().getAliadoCercano();
        GetComponent<MeleeController>().Moverse(aliado.transform.position);
        GetComponent<PersonajeController>().pidiendoAuxilio();

        Debug.Log(gameObject.name + "Estoy Pidiendo auxilio");

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }

    private ReturnValues PedirAuxilioSuccessCheck() 
    {
        if (!GetComponent<PersonajeController>().pidoAuxilio())
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }
}
