using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using System;

public class BTRanger : BTAbstracto
{

    public BTRanger() : base()
    {
        this.BT = new BehaviourTreeEngine(false);

        this.CrearIA();
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.BT.Active = false;
    }

    // Update is called once per frame
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

        #region Primera rama
        SequenceNode provocandoSecuencia = BT.CreateSequenceNode("ProvocandoSec", false);

        LeafNode enemigoProvocandoPerception = BT.CreateLeafNode("EnemigoProvocando", EnemigoProvocandoAction, EnemigoProvocandoSuccessCheck);
        SelectorNode decisionProvocando = BT.CreateSelectorNode("DecisionProvocando");
        provocandoSecuencia.AddChild(enemigoProvocandoPerception);
        provocandoSecuencia.AddChild(decisionProvocando);

        SequenceNode secuenciaProvocando = BT.CreateSequenceNode("SecuenciaProvocando", false);
        LeafNode moverseAccion = BT.CreateLeafNode("Moverse", MoverseEnemigoAction, MoverseEnemigoSuccessCheck);
        decisionProvocando.AddChild(secuenciaProvocando);
        decisionProvocando.AddChild(moverseAccion);

        LeafNode enemigoRangoPercepcion = BT.CreateLeafNode("EnemigoRango", EnemigoARangoAction, EnemigoARangoSuccessCheck);
        LeafNode atacarAccion = BT.CreateLeafNode("Atacar", AtacarAction, AtacarSuccessCheck);
        secuenciaProvocando.AddChild(enemigoRangoPercepcion);
        secuenciaProvocando.AddChild(atacarAccion);
        #endregion

        #region Segunda rama
        SequenceNode enemigoARangoSecuencia = BT.CreateSequenceNode("EnemigoARangoSec", false);

        //LeafNode enemigoRangoPercepcion = BT.CreateLeafNode("EnemigoRango", EnemigoARangoAction, EnemigoARangoSuccessCheck);
        SelectorNode decisionEnemigoARango = BT.CreateSelectorNode("DecisionProvocando");
        enemigoARangoSecuencia.AddChild(enemigoRangoPercepcion);
        enemigoARangoSecuencia.AddChild(decisionEnemigoARango);


        SequenceNode secuenciaSuficienteVida = BT.CreateSequenceNode("SecuenciaProvocando", false);
        decisionEnemigoARango.AddChild(secuenciaSuficienteVida);

        LeafNode suficienteVidaPerception = BT.CreateLeafNode("SuficienteVida", SuficienteVidaAction, SuficienteVidaSuccessCheck);
        //LeafNode atacarAccion = BT.CreateLeafNode("Atacar", AtacarAction, AtacarSuccessCheck);
        secuenciaSuficienteVida.AddChild(suficienteVidaPerception);
        secuenciaSuficienteVida.AddChild(atacarAccion);


        SelectorNode decisionEnemigoPocaVida = BT.CreateSelectorNode("decisionEnemigoPocaVida");
        decisionEnemigoARango.AddChild(decisionEnemigoPocaVida);

        SequenceNode secuenciaEnemigoPocaVida = BT.CreateSequenceNode("secuenciaEnemigoPocaVida", false);
        decisionEnemigoPocaVida.AddChild(secuenciaEnemigoPocaVida);
        LeafNode enemigoPocaVidaPercerpcion = BT.CreateLeafNode("EnemigoPocaVida", EnemigoPocaVidaAction, EnemigoPocaVidaSuccessCheck);
        //LeafNode atacarAccion = BT.CreateLeafNode("Atacar", AtacarAction, AtacarSuccessCheck);
        secuenciaEnemigoPocaVida.AddChild(enemigoPocaVidaPercerpcion);
        secuenciaEnemigoPocaVida.AddChild(atacarAccion);
        LeafNode pedirAuxilioAccion = BT.CreateLeafNode("PedirAuxilio", PedirAuxilioAction, PedirAuxilioSuccessCheck);
        decisionEnemigoPocaVida.AddChild(pedirAuxilioAccion);

        #endregion

        #region Tercera rama
        SelectorNode noSuficienteVidaSelector = BT.CreateSelectorNode("NoSuficienteVidaSelec");

        #region Subrama 1
        SequenceNode noSuficienteVidaSecuencia = BT.CreateSequenceNode("NoSuficienteVidaSec", false);
        noSuficienteVidaSelector.AddChild(noSuficienteVidaSecuencia);

        LeafNode noSuficienteVidaPerception = BT.CreateLeafNode("NoSuficienteVida", NoSuficienteVidaAction, NoSuficienteVidaSuccessCheck);
        SelectorNode vidageneradaSelector = BT.CreateSelectorNode("VidageneradaSelec");
        noSuficienteVidaSecuencia.AddChild(noSuficienteVidaPerception);
        noSuficienteVidaSecuencia.AddChild(vidageneradaSelector);

        SequenceNode vidaGeneradaSecuencia = BT.CreateSequenceNode("VidaGeneradaSec", false);
        //LeafNode pedirAuxilioAccion = BT.CreateLeafNode("PedirAuxilioVida", PedirAuxilioAction, PedirAuxilioSuccessCheck);
        vidageneradaSelector.AddChild(vidaGeneradaSecuencia);
        vidageneradaSelector.AddChild(pedirAuxilioAccion);

        LeafNode vidaGeneradaPerception = BT.CreateLeafNode("VidaGenerada", VidaGeneradaAction, VidaGeneradaSuccessCheck);
        LeafNode moverseVidaAccion = BT.CreateLeafNode("MoverseVida", MoverseVidaAction, MoverseVidaSuccessCheck);
        vidaGeneradaSecuencia.AddChild(vidaGeneradaPerception);
        vidaGeneradaSecuencia.AddChild(moverseVidaAccion);
        #endregion

        #region Subrama 2
        SequenceNode especialCargadoSecuencia = BT.CreateSequenceNode("EspecialCargadoSec", false);
        noSuficienteVidaSelector.AddChild(especialCargadoSecuencia);

        LeafNode especialCargadoPerception = BT.CreateLeafNode("EspecialCargado", EspecialCargadoAction, EspecialCargadoSuccessCheck);
        LeafNode inspirarAccion = BT.CreateLeafNode("Inspirar", InspirarAction, InspirarSuccessCheck);
        especialCargadoSecuencia.AddChild(especialCargadoPerception);
        especialCargadoSecuencia.AddChild(inspirarAccion);
        #endregion

        #region Subrama 3
        SelectorNode torreAsequibleSelector = BT.CreateSelectorNode("TorreAsequibleSelec");
        noSuficienteVidaSelector.AddChild(torreAsequibleSelector);

        SequenceNode torreAsequibleSecuencia = BT.CreateSequenceNode("TorreAsequibleSec", false);
        LeafNode moverseEnemigoAccion = BT.CreateLeafNode("Moverse", MoverseEnemigoAction, MoverseEnemigoSuccessCheck);
        torreAsequibleSelector.AddChild(torreAsequibleSecuencia);
        torreAsequibleSelector.AddChild(moverseEnemigoAccion);


        LeafNode torreAsequiblePercepcion = BT.CreateLeafNode("TorreAsequible", TorreAsequibleAction, TorreAsequibleSuccessCheck);
        LeafNode torreVaciaPercepcion = BT.CreateLeafNode("TorreVacia", TorreVaciaAction, TorreVaciaSuccessCheck);
        LeafNode subirTorreAccion = BT.CreateLeafNode("SubirTorre", SubirTorreAction, SubirTorreSuccessCheck);
        torreAsequibleSecuencia.AddChild(torreAsequiblePercepcion);
        torreAsequibleSecuencia.AddChild(torreVaciaPercepcion);
        torreAsequibleSecuencia.AddChild(subirTorreAccion);
        #endregion

        #endregion

        LoopDecoratorNode mainLoop = BT.CreateLoopNode("loop", nodoRoot);

        nodoRoot.AddChild(provocandoSecuencia);
        nodoRoot.AddChild(enemigoARangoSecuencia);
        nodoRoot.AddChild(noSuficienteVidaSelector);

        // Establecer RaÃ­z
        BT.SetRootNode(nodoRoot);
    }

    #region Provocado Secuencia
    private void EnemigoProvocandoAction() { }

    private ReturnValues EnemigoProvocandoSuccessCheck()
    {
        if (GetComponent<PersonajeController>().AlguienProvocando())
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }

        //return ReturnValues.Failed;

    }

    private void EnemigoARangoProvocandoAction(){ }
    private ReturnValues EnemigoARangoProvocandoSuccessCheck()
    {
        enemigo = GetComponent<MeleeController>().GetPersonaje().EnemigoARango();

        if (enemigo != null)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

}
