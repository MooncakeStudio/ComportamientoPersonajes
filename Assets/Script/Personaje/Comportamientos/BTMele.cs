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
        this.BT.Active = false;
        //StartCoroutine(ejecutarArbol());
    }

    private void FixedUpdate()
    {
        BT.Update();
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
        LeafNode enemigoRangoPercepcion = BT.CreateLeafNode("EnemigoRango", EnemigoARangoProvocandoAction, EnemigoARangoProvocandoSuccessCheck);
        LeafNode atacarAccion = BT.CreateLeafNode("Atacar", AtaqueProvocando, AtaqueProvocandoSuccessCheck);
        LeafNode moverseAccion = BT.CreateLeafNode("Moverse", MoviendoAEnemigo, MoviendoAEnemigoSuccessCheck);

        //Nodos Provocar
        LeafNode aliadoAuxilio = BT.CreateLeafNode("AliadoAuxilio", AliadoAuxilioAction, AliadoAuxilioSuccessCheck);
        LeafNode especialCargado = BT.CreateLeafNode("EspecialCargado", EspecialCargadoAction, EspecialCargadoSuccessCheck);
        LeafNode SuficienteVida = BT.CreateLeafNode("SuficienteVida", SuficienteVidaAction, SuficienteVidaSuccessCheck);
        LeafNode provocarAccion = BT.CreateLeafNode("Provocar", ProvocarAction, ProvocarSuccessCheck);

        //Nodos Atacar
        LeafNode EnemigoARango = BT.CreateLeafNode("EnemigoARango", EnemigoARangoAction, EnemigoARangoSuccessCheck);
        SelectorNode selecAtaque1 = BT.CreateSelectorNode("SelectorAtaque1");
        SequenceNode secAtaque1 = BT.CreateSequenceNode("SecAtaque1", false);
        LeafNode suficienteVidaAtaque = BT.CreateLeafNode("VidaAataque", SuficienteVidaAtaqueAction, SuficienteVidaAtaqueSuccessCheck);
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
        LeafNode auxilio = BT.CreateLeafNode("AuxilioPedir", PedirAuxilioMoverseAction, PedirAuxilioMoverseSuccessCheck);
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
        ataqueSecuencia.AddChild(EnemigoARango);
        ataqueSecuencia.AddChild(selecAtaque1);
        selecAtaque1.AddChild(secAtaque1);
        selecAtaque1.AddChild(selecAtaque2);
        secAtaque1.AddChild(suficienteVidaAtaque);
        secAtaque1.AddChild(atacarAtaque);
        selecAtaque2.AddChild(secAtaque2);
        selecAtaque2.AddChild(pedirAuxilioAccion);
        secAtaque2.AddChild(EnemigoPocaVida);
        secAtaque2.AddChild(atacarEnemigo);

        //Añadir hijos moverse
        moverseSelector.AddChild(secMoverse1);
        moverseSelector.AddChild(moverse);
        secMoverse1.AddChild(PocaVida);
        secMoverse1.AddChild(selecMoverse);
        selecMoverse.AddChild(vidaGenerado);
        selecMoverse.AddChild(auxilio);
        vidaGenerado.AddChild(VidaGenerada);
        vidaGenerado.AddChild(IrAVida);

        nodoRoot.AddChild(provocandoSecuencia);
        nodoRoot.AddChild(provocarSecuencia);
        nodoRoot.AddChild(ataqueSecuencia);
        nodoRoot.AddChild(moverseSelector);

        // Establecer Raíz
        BT.SetRootNode(mainLoop);

    }

    /* ACCIONES */

    #region Provocado Secuencia
    private void EnemigoProvocandoAction() { }

    private ReturnValues EnemigoProvocandoSuccessCheck()
    {
        if (GetComponent<PersonajeController>().AlguienProvocando())
        {
            //StartCoroutine(muestraBocadillo(false, "Provocando"));
            return ReturnValues.Succeed;

        }
        else
        {
            return ReturnValues.Failed;
        }

        //return ReturnValues.Failed;

    }

    private void EnemigoARangoProvocandoAction() { }
    private ReturnValues EnemigoARangoProvocandoSuccessCheck()
    {
        enemigo = GetComponent<MeleeController>().GetPersonaje().EnemigoARango();

        if (enemigo != null)
        {
            //StartCoroutine(muestraBocadillo(false, "Enemigo a rango"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void MoviendoAEnemigo()
    {
        var enemigo = GetComponent<PersonajeController>().getEnemigoObjetivo();

        GetComponent<MeleeController>().Moverse(enemigo.transform.position);

        StartCoroutine(muestraBocadillo(true, "Me muevo"));

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }

    private ReturnValues MoviendoAEnemigoSuccessCheck()
    {
        enemigo = GetComponent<MeleeController>().GetPersonaje().EnemigoARango();

        if (enemigo != null)
        {
            StartCoroutine(muestraBocadillo(true, "Me muevo a enemigo"));
            return ReturnValues.Failed;
        }
        else
        {
            return ReturnValues.Succeed;
        }
    }

    private void AtaqueProvocando()
    {
        Debug.Log(gameObject.name + " ataca");
        GetComponent<MeleeController>().GetPersonaje().Atacar(enemigo);
        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }

    private ReturnValues AtaqueProvocandoSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "He atacado"));
        return ReturnValues.Succeed;
    }
    #endregion

    #region Provocar Secuencia
    private void AliadoAuxilioAction() { }
    private ReturnValues AliadoAuxilioSuccessCheck()
    {
        if (GetComponent<PersonajeController>().alguienPidiendoAuxilio())
        {
            //StartCoroutine(muestraBocadillo(false, "Ayudo"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }

    }

    private void EspecialCargadoAction() { }

    private ReturnValues EspecialCargadoSuccessCheck()
    {
        /*if (GetComponent<PersonajeController>().tengoAtaqueEspecial())
        {
            Debug.Log(gameObject.name + " Especial cargado");
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }*/
        //StartCoroutine(muestraBocadillo(false, "Especial cargado"));
        return ReturnValues.Succeed;
    }

    private void SuficienteVidaAction() { }
    private ReturnValues SuficienteVidaSuccessCheck()
    {
        if (GetComponent<MeleeController>().GetPersonaje().GetVida() > 20)
        {
            //StartCoroutine(muestraBocadillo(false, "Suficiente Vida"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void ProvocarAction()
    {
        GetComponent<MeleeController>().Provocar();
        //var aliado = GetComponent<PersonajeController>().getAliadoCercano();
        //GetComponent<MeleeController>().Moverse(aliado.transform.position);

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
        //GetComponent<PersonajeController>().FinTurno();
    }
    private ReturnValues ProvocarSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "Provoco"));
        return ReturnValues.Succeed;
    }
    #endregion

    #region Atacar Secuencia
    private void EnemigoARangoAction() { }
    private ReturnValues EnemigoARangoSuccessCheck()
    {
        enemigo = GetComponent<MeleeController>().GetPersonaje().EnemigoARango();

        if (enemigo != null)
        {
            //StartCoroutine(muestraBocadillo(false, "Enemigo a rango"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
        //return ReturnValues.Succeed;
    }

    private void SuficienteVidaAtaqueAction() { }
    private ReturnValues SuficienteVidaAtaqueSuccessCheck()
    {
        if (GetComponent<MeleeController>().GetPersonaje().GetVida() > 20)
        {
            //StartCoroutine(muestraBocadillo(false, "Suficiente vida"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void AtacarAction()
    {
        Debug.Log(gameObject.name + " ataca");
        GetComponent<MeleeController>().GetPersonaje().Atacar(enemigo);
        Debug.Log(enemigo.name + " tiene vida: " + enemigo.GetPersonaje().GetVida());

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
        //GetComponent<PersonajeController>().FinTurno();
    }
    private ReturnValues AtacarSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "Ataco"));
        return ReturnValues.Succeed;
    }

    private void EnemigoPocaVidaAction() { }

    private ReturnValues EnemigoPocaVidaSuccessCheck()
    {
        if (enemigo.GetComponent<PersonajeController>().GetPersonaje().GetVida() <
            GetComponent<MeleeController>().GetPersonaje().GetVida())
        {
            //StartCoroutine(muestraBocadillo(false, "Enemigo tiene poca vida"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void PedirAuxilioAction()
    {

        var aliado = GetComponent<PersonajeController>().getAliadoCercano();
        GetComponent<MeleeController>().Moverse(aliado.transform.position);
        GetComponent<PersonajeController>().pidiendoAuxilio();
        //GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }
    private ReturnValues PedirAuxilioSuccessCheck()
    {
        /*if (!GetComponent<PersonajeController>().pidoAuxilio())
        {
            Debug.Log(gameObject.name+ " Pido auxilio");
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }*/
        StartCoroutine(muestraBocadillo(true, "¡AUXILIO!"));
        return ReturnValues.Succeed;
    }
    #endregion

    #region Moverse Secuencia
    private void MoverseEnemigoAction()
    {
        var enemigo = GetComponent<PersonajeController>().getEnemigoObjetivo();

        GetComponent<MeleeController>().Moverse(enemigo.transform.position);



        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }
    private ReturnValues MoverseEnemigoSuccessCheck()
    {
        enemigo = GetComponent<MeleeController>().GetPersonaje().EnemigoARango();

        if (enemigo != null)
        {
            return ReturnValues.Failed;
        }
        else
        {
            StartCoroutine(muestraBocadillo(true, "Me muevo hacia el enemigo"));
            return ReturnValues.Succeed;
        }

        //return ReturnValues.Succeed;
    }

    private void PocaVidaAction() { }
    private ReturnValues PocaVidaSuccessCheck()
    {
        if (GetComponent<MeleeController>().GetPersonaje().GetVida() <= 20)
        {
            //StartCoroutine(muestraBocadillo(false, "Tengo poca vida"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void PedirAuxilioMoverseAction()
    {
        var aliado = GetComponent<PersonajeController>().getAliadoCercano();
        GetComponent<MeleeController>().Moverse(aliado.transform.position);
        GetComponent<PersonajeController>().pidiendoAuxilio();
        //GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }
    private ReturnValues PedirAuxilioMoverseSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "¡AUXILIO!"));
        return ReturnValues.Succeed;
    }

    private void VidaGeneradaAction() { }
    private ReturnValues VidaGeneradaSuccessCheck()
    {
        if (GameManager.hayObj)
        {
            //StartCoroutine(muestraBocadillo(false, "Vida generada"));
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

        var movimientoManager = GetComponent<MeleeController>();

        movimientoManager.Moverse(GameManager.objetivo);

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }
    private ReturnValues IrAVidaSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "Me muevo a vida"));
        return ReturnValues.Succeed;
    }
    #endregion


}
