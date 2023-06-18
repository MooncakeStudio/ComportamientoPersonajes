using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMago : BTAbstracto
{

    PersonajeController aliadoCercano;
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
        LeafNode pedirAuxilioAtaque = BT.CreateLeafNode("AuxilioAtaque", PedirAuxilioAction, PedirAuxilioSuccessCheck);
        LeafNode enemigoPocaVida = BT.CreateLeafNode("EnemigoPocaVida", EnemigoPocaVidaAction, EnemigoPocaVidaSuccessCheck);
        LeafNode atacarAccion2 = BT.CreateLeafNode("AtaqueAtacar2", AtacarAction, AtacarSuccessCheck);

        //Secuencia Moverse
        SequenceNode secuenciaMoverse1 = BT.CreateSequenceNode("SecuenciaMoverse1", false);
        LeafNode noSuficienteVidaPerception = BT.CreateLeafNode("NoSuficienteVida", NoSuficienteVidaAction, NoSuficienteVidaSuccessCheck);
        LeafNode moverrseMoverseAccion = BT.CreateLeafNode("MoverseMoverse", MoverseEnemigoAction, MoverseEnemigoSuccessCheck);
        SelectorNode selectorMoverse = BT.CreateSelectorNode("SelectorMoverse");
        SequenceNode secuenciaMoverse2 = BT.CreateSequenceNode("SecuenciaMoverse2", false);
        LeafNode pedirAuxilioMoverse = BT.CreateLeafNode("AuxilioMoverse", PedirAuxilioAction, PedirAuxilioSuccessCheck);
        LeafNode vidaGeneradaPerception = BT.CreateLeafNode("VidaGenerada", VidaGeneradaAction, VidaGeneradaSuccessCheck);
        LeafNode moverseVidaAccion = BT.CreateLeafNode("MoverseVida", MoverseVidaAction, MoverseVidaSuccessCheck);

        LoopDecoratorNode mainLoop = BT.CreateLoopNode("loop", nodoRoot);

        provocandoSecuencia.AddChild(enemigoProvocandoPerception);
        provocandoSecuencia.AddChild(decisionProvocando);
        decisionProvocando.AddChild(secuenciaProvocando);
        decisionProvocando.AddChild(moverseAccion);
        secuenciaProvocando.AddChild(enemigoRangoPercepcion);
        secuenciaProvocando.AddChild(atacarAccion);

        curarSecuencia.AddChild(aliadoAuxilioPerception);
        curarSecuencia.AddChild(selectorCurar);
        selectorCurar.AddChild(secuenciaCura);
        selectorCurar.AddChild(moverseAliadoAccion);
        secuenciaCura.AddChild(aliadoRangoPerception);
        secuenciaCura.AddChild(especialCargadoPerception);
        secuenciaCura.AddChild(curarAccion);

        ataqueSecuencia.AddChild(enemigoRangoAtacarPerception);
        ataqueSecuencia.AddChild(selectorAtaque1);
        selectorAtaque1.AddChild(secuenciaAtacque1);
        selectorAtaque1.AddChild(selectorAtaque2);
        secuenciaAtacque1.AddChild(suficienteVidaPerception);
        secuenciaAtacque1.AddChild(atacarAccion1);
        selectorAtaque2.AddChild(secuenciaAtaque2);
        selectorAtaque2.AddChild(pedirAuxilioAtaque);
        secuenciaAtaque2.AddChild(enemigoPocaVida);
        secuenciaAtaque2.AddChild(atacarAccion2);

        moverseSelector.AddChild(secuenciaMoverse1);
        moverseSelector.AddChild(moverrseMoverseAccion);
        secuenciaMoverse1.AddChild(noSuficienteVidaPerception);
        secuenciaMoverse1.AddChild(selectorMoverse);
        selectorMoverse.AddChild(secuenciaMoverse2);
        selectorMoverse.AddChild(pedirAuxilioMoverse);
        secuenciaMoverse2.AddChild(vidaGeneradaPerception);
        secuenciaMoverse2.AddChild(moverseVidaAccion);

        nodoRoot.AddChild(provocandoSecuencia);
        nodoRoot.AddChild(curarSecuencia);
        nodoRoot.AddChild(ataqueSecuencia);
        nodoRoot.AddChild(moverseSelector);

        // Establecer Raíz
        BT.SetRootNode(nodoRoot);
    }

    #region Provocado Secuencia
    private void EnemigoProvocandoAction() { }
    private ReturnValues EnemigoProvocandoSuccessCheck()
    {
        if (GetComponent<PersonajeController>().AlguienProvocando())
        {
            //StartCoroutine(muestraBocadillo(false, "Enemigo provocando"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void EnemigoARangoAction() { }
    private ReturnValues EnemigoARangoSuccessCheck()
    {
        enemigo = GetComponent<MagoController>().GetPersonaje().EnemigoARango();

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

    private void AtacarAction()
    {
        GetComponent<MagoController>().GetPersonaje().Atacar(enemigo);

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }
    private ReturnValues AtacarSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "Ataque"));
        return ReturnValues.Succeed;
    }

    private void MoverseEnemigoAction()
    {
        var enemigo = GetComponent<PersonajeController>().getEnemigoObjetivo();

        GetComponent<MagoController>().Moverse(enemigo.transform.position);

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }
    private ReturnValues MoverseEnemigoSuccessCheck()
    {
        enemigo = GetComponent<MagoController>().GetPersonaje().EnemigoARango();

        if (enemigo != null)
        {
            return ReturnValues.Failed;
        }
        else
        {
            StartCoroutine(muestraBocadillo(true, "Me muevo a enemigo"));
            return ReturnValues.Succeed;
        }
    }
    #endregion

    #region Curar Secuencia
    private void AliadoAuxilioAction() { }
    private ReturnValues AliadoAuxilioSuccessCheck()
    {
        if (GetComponent<PersonajeController>().alguienPidiendoAuxilio())
        {
            //StartCoroutine(muestraBocadillo(false, "Aliado pide auxilio"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void AliadoARangoAction() { }
    private ReturnValues AliadoARangoSuccessCheck()
    {
        var aliado = GetComponent<MagoController>().GetPersonaje() as Mago;
        this.aliadoCercano = aliado.AliadoARango();
        if (aliadoCercano != null)
        {
            StartCoroutine(muestraBocadillo(true, "Aliado a rango"));
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
        //StartCoroutine(muestraBocadillo(false, "Especial cargado"));

        return ReturnValues.Succeed;
    }

    private void CurarAction()
    {
        GetComponent<MagoController>().Curar(aliadoCercano);

        GetComponent<PersonajeController>().FinTurno();
        GetBT().Active = false;
    }
    private ReturnValues CurarSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "Curar"));
        return ReturnValues.Succeed;
    }

    private void MoverseAliadoAction()
    {
        var aliado = GetComponent<PersonajeController>().getAliadoCercano();
        GetComponent<MagoController>().Moverse(aliado.transform.position);

        GetComponent<PersonajeController>().FinTurno();
        GetBT().Active = false;
    }
    private ReturnValues MoverseAliadoSuccessCheck()
    {
        var aliado = GetComponent<MagoController>().GetPersonaje() as Mago;
        this.aliadoCercano = aliado.AliadoARango();
        if (aliadoCercano != null)
        {
            return ReturnValues.Failed;
        }
        else
        {
            StartCoroutine(muestraBocadillo(true, "Me muevo a aliado"));
            return ReturnValues.Succeed;
        }
    }
    #endregion

    #region Atacar Secuencia
    private void SuficienteVidaAction() { }
    private ReturnValues SuficienteVidaSuccessCheck()
    {
        if (GetComponent<MagoController>().GetPersonaje().GetVida() > 20)
        {
            //StartCoroutine(muestraBocadillo(false, "Suficiente vida"));
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void EnemigoPocaVidaAction() { }
    private ReturnValues EnemigoPocaVidaSuccessCheck()
    {
        if (enemigo.GetComponent<PersonajeController>().GetPersonaje().GetVida() <
            GetComponent<MagoController>().GetPersonaje().GetVida())
        {
            //StartCoroutine(muestraBocadillo(false, "Enemigo poca vida"));
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
        this.GetBT().Active = false;
    }
    private ReturnValues PedirAuxilioSuccessCheck()
    {
        //StartCoroutine(muestraBocadillo(false, "¡AUXILIO!"));
        return ReturnValues.Succeed;
    }
    #endregion

    #region Moverse Secuencia
    private void NoSuficienteVidaAction() { }
    private ReturnValues NoSuficienteVidaSuccessCheck()
    {
        if (GetComponent<MagoController>().GetPersonaje().GetVida() <= 20)
        {
            //StartCoroutine(muestraBocadillo(false, "No suficiente vida"));
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

    private void MoverseVidaAction()
    {
        var movimientoManager = GetComponent<MagoController>();

        movimientoManager.Moverse(GameManager.objetivo);

        GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }
    private ReturnValues MoverseVidaSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "Me muevo a vida"));
        return ReturnValues.Succeed;
    }

    #endregion
}
