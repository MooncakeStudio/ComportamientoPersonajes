using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMago : BTAbstracto
{
    public BTMago() : base()
    {
        this.BT = new BehaviourTreeEngine(false);

        this.CrearIA();
    }


    // METODOS

    private void Start()
    {
        this.BT.Active = false;
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
        SequenceNode curarSecuencia = BT.CreateSequenceNode("CurarSec", false);
        SequenceNode ataqueSecuencia = BT.CreateSequenceNode("AtaqueSec", false);
        SelectorNode moverseSelector = BT.CreateSelectorNode("MoverseSelec");

        //Secuencia Provocado
        LeafNode enemigoProvocandoPerception = BT.CreateLeafNode("EnemigoProvocando", EnemigoProvocandoAction, EnemigoProvocandoSuccessCheck);
        SelectorNode decisionProvocando = BT.CreateSelectorNode("DecisionProvocando");
        SequenceNode secuenciaProvocando = BT.CreateSequenceNode("SecuenciaProvocando", false);
        LeafNode enemigoRangoPercepcion = BT.CreateLeafNode("EnemigoRango", EnemigoARangoAction, EnemigoARangoSuccessCheck);
        LeafNode atacarAccion = BT.CreateLeafNode("Atacar", AtacarAction, AtacarSuccessCheck);
        LeafNode moverseAccion = BT.CreateLeafNode("Moverse", MoverseEnemigoAction, MoverseEnemigoSuccessCheck);

        //Secuencia Curar
        LeafNode aliadoAuxilioPerception = BT.CreateLeafNode("AliadoAuxilio", AliadoAuxilioAction, AliadoAuxilioSuccessCheck);
        SelectorNode selectorCurar = BT.CreateSelectorNode("SelectorCurar");
        SequenceNode secuenciaCura = BT.CreateSequenceNode("SecuenciaCura", false);
        LeafNode moverseAliadoAccion = BT.CreateLeafNode("MoverseAliado", MoverseAliadoAction, MoverseAliadoSuccessCheck);
        LeafNode aliadoRangoPerception = BT.CreateLeafNode("AliadoRango", AliadoARangoAction, AliadoARangoSuccessCheck);
        LeafNode especialCargadoPerception = BT.CreateLeafNode("EspecialCargado", EspecialCargadoAction, EspecialCargadoSuccessCheck);
        LeafNode curarAccion = BT.CreateLeafNode("Curar", CurarAction, CurarSuccessCheck);

        //Secuencia Atacar
        LeafNode enemigoRangoAtacarPerception = BT.CreateLeafNode("EnemigoRangoAtacar", EnemigoARangoAction, EnemigoARangoSuccessCheck);
        SelectorNode selectorAtaque1 = BT.CreateSelectorNode("SelectorAtaque1");
        SequenceNode secuenciaAtacque1 = BT.CreateSequenceNode("SecuenciaAtaque1", false);
        SelectorNode selectorAtaque2 = BT.CreateSelectorNode("SelectorAtaque2");
        LeafNode suficienteVidaPerception = BT.CreateLeafNode("SuficienteVida", SuficienteVidaAction, SuficienteVidaSuccessCheck);
        LeafNode atacarAccion1 = BT.CreateLeafNode("AtaqueAtacar1", AtacarAction, AtacarSuccessCheck);
        SequenceNode secuenciaAtaque2 = BT.CreateSequenceNode("SecuenciaAtaque2", false);
        LeafNode pedirAuxilioAtaque = BT.CreateLeafNode("AuxilioAtaque", PedirAxuilioAction, PedirAuxilioSuccessCheck);
        LeafNode enemigoPocaVida = BT.CreateLeafNode("EnemigoPocaVida", EnemigoPocaVidaAction, EnemigoPocaVidaSuccessCheck);
        LeafNode atacarAccion2 = BT.CreateLeafNode("AtaqueAtacar2", AtacarAction, AtacarSuccessCheck);

        //Secuencia Moverse
        SequenceNode secuenciaMoverse1 = BT.CreateSequenceNode("SecuenciaMoverse1", false);

        nodoRoot.AddChild(provocandoSecuencia);
        nodoRoot.AddChild(curarSecuencia);
        nodoRoot.AddChild(ataqueSecuencia);
        nodoRoot.AddChild(moverseSelector);

        // Establecer Raíz
        BT.SetRootNode(nodoRoot);
    }

    #region Provocado Secuencia
    private void EnemigoProvocandoAction() { }
    private ReturnValues EnemigoProvocandoSuccessCheck() { return ReturnValues.Failed; }

    private void EnemigoARangoAction() { }
    private ReturnValues EnemigoARangoSuccessCheck() { return ReturnValues.Failed; }

    private void AtacarAction() { }
    private ReturnValues AtacarSuccessCheck() { return ReturnValues.Failed; }

    private void MoverseEnemigoAction() { }
    private ReturnValues MoverseEnemigoSuccessCheck() { return ReturnValues.Failed; }
    #endregion

    #region Curar Secuencia
    private void AliadoAuxilioAction() { }
    private ReturnValues AliadoAuxilioSuccessCheck() { return ReturnValues.Failed; }
    
    private void AliadoARangoAction() { }
    private ReturnValues AliadoARangoSuccessCheck() { return ReturnValues.Failed; }

    private void EspecialCargadoAction() { }
    private ReturnValues EspecialCargadoSuccessCheck() { return ReturnValues.Failed; }

    private void CurarAction() { }
    private ReturnValues CurarSuccessCheck() { return ReturnValues.Failed; }

    private void MoverseAliadoAction() { }
    private ReturnValues MoverseAliadoSuccessCheck() { return ReturnValues.Failed; }
    #endregion

    #region Atacar Secuencia
    private void SuficienteVidaAction() { }
    private ReturnValues SuficienteVidaSuccessCheck() { return ReturnValues.Failed; }

    private void EnemigoPocaVidaAction() { }
    private ReturnValues EnemigoPocaVidaSuccessCheck() { return ReturnValues.Failed; }

    private void PedirAxuilioAction() { }
    private ReturnValues PedirAuxilioSuccessCheck() { return ReturnValues.Failed; }
    #endregion

    #region Moverse Secuencia
    private void NoSuficienteVidaAction() { }
    private ReturnValues NoSuficienteVidaSuccessCheck() { return ReturnValues.Failed; }

    private void VidaGeneradaAction() { }
    private ReturnValues VidaGeneradaSuccessCheck() { return ReturnValues.Failed; }

    #endregion
}
