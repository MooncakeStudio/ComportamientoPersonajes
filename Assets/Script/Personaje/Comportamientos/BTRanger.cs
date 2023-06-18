using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTRanger : BTAbstracto
{

    #region variables

    bool aliadosCerca;

    private BehaviourTreeEngine BT;


    private SelectorNode RootSelector;
    private SequenceNode ProvocandoSec;
    private LeafNode EnemigoProvocando;
    private SelectorNode DecisionProvocando;
    private SequenceNode SecuenciaProvocando;
    private LeafNode Moverse;
    private LeafNode EnemigoRango;
    private LeafNode AtacarProvocado;
    private SequenceNode EnemigoARangoSec;
    private LeafNode EnemigoRangoAtaque;
    private SelectorNode SelectorAtaque;
    private SequenceNode SecVidaAtaque;
    private LeafNode SuficienteVida;
    private LeafNode Atacar;
    private SelectorNode DecisionPocaVida;
    private SequenceNode SecPocaVida;
    private LeafNode EnemigoPocaVida;
    private LeafNode AtacarPocaVida;
    private LeafNode Auxilio;
    private SelectorNode DecisionMovimiento;
    private SequenceNode SecVida;
    private LeafNode NoSuficienteVida;
    private SelectorNode DecisionObjVida;
    private SequenceNode SecVidaGenerada;
    private LeafNode VidaGenerada;
    private LeafNode MoverseVida;
    private LeafNode AuxilioNoOBjVida;
    private SequenceNode SecInspirar;
    private LeafNode EspecialCargado;
    private SelectorNode DecisionInspirar;
    private SequenceNode ComprobarInspirar;
    private LeafNode AliadoARango;
    private LeafNode Inspirar;
    private LeafNode MoverAAliado;
    private SelectorNode DecisionMoverse;
    private SequenceNode SecTorre;
    private LeafNode TorreAsequible;
    private LeafNode TorreVacia;
    private LeafNode SubirTorre;
    private LeafNode MoverseEnemigo;

    //Place your variables here

    #endregion variables

    // Start is called before the first frame update
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

    public override IEnumerator ejecutarArbol()
    {
        BT.Update();

        yield return new WaitForSeconds(GetComponent<PersonajeController>().GetVelocidad() / 100);

        //BT.Reset();
        StartCoroutine(ejecutarArbol());
    }


    public override void CrearIA()
    {
        //Nodo root
        RootSelector = BT.CreateSelectorNode("RootSelector");

        // Nodes
        ProvocandoSec = BT.CreateSequenceNode("ProvocandoSec", false);
        EnemigoProvocando = BT.CreateLeafNode("EnemigoProvocando", EnemigoProvocandoAction, EnemigoProvocandoSuccessCheck);
        DecisionProvocando = BT.CreateSelectorNode("DecisionProvocando");
        SecuenciaProvocando = BT.CreateSequenceNode("SecuenciaProvocando", true);
        Moverse = BT.CreateLeafNode("Moverse", MoverseAction, MoverseSuccessCheck);
        EnemigoRango = BT.CreateLeafNode("EnemigoRango", EnemigoRangoAction, EnemigoRangoSuccessCheck);
        AtacarProvocado = BT.CreateLeafNode("AtacarProvocado", AtacarAction, AtacarSuccessCheck);
        EnemigoARangoSec = BT.CreateSequenceNode("EnemigoARangoSec", false);
        EnemigoRangoAtaque = BT.CreateLeafNode("EnemigoRangoAtaque", EnemigoRangoAction, EnemigoRangoSuccessCheck);
        SelectorAtaque = BT.CreateSelectorNode("SelectorAtaque");
        SecVidaAtaque = BT.CreateSequenceNode("SecVidaAtaque", false);
        SuficienteVida = BT.CreateLeafNode("SuficienteVida", SuficienteVidaAction, SuficienteVidaSuccessCheck);
        Atacar = BT.CreateLeafNode("Atacar", AtacarAction, AtacarSuccessCheck);
        DecisionPocaVida = BT.CreateSelectorNode("DecisionPocaVida");
        SecPocaVida = BT.CreateSequenceNode("SecPocaVida", false);
        EnemigoPocaVida = BT.CreateLeafNode("EnemigoPocaVida", EnemigoPocaVidaAction, EnemigoPocaVidaSuccessCheck);
        AtacarPocaVida = BT.CreateLeafNode("AtacarPocaVida", AtacarAction, AtacarSuccessCheck);
        Auxilio = BT.CreateLeafNode("Auxilio", AuxilioAction, AuxilioSuccessCheck);
        DecisionMovimiento = BT.CreateSelectorNode("DecisionMovimiento");
        SecVida = BT.CreateSequenceNode("SecVida", false);
        NoSuficienteVida = BT.CreateLeafNode("NoSuficienteVida", NoSuficienteVidaAction, NoSuficienteVidaSuccessCheck);
        DecisionObjVida = BT.CreateSelectorNode("DecisionObjVida");
        SecVidaGenerada = BT.CreateSequenceNode("SecVidaGenerada", false);
        VidaGenerada = BT.CreateLeafNode("VidaGenerada", VidaGeneradaAction, VidaGeneradaSuccessCheck);
        MoverseVida = BT.CreateLeafNode("MoverseVida", MoverseVidaAction, MoverseVidaSuccessCheck);
        AuxilioNoOBjVida = BT.CreateLeafNode("AuxilioNoOBjVida", AuxilioAction, AuxilioSuccessCheck);
        SecInspirar = BT.CreateSequenceNode("SecInspirar", false);
        EspecialCargado = BT.CreateLeafNode("EspecialCargado", EspecialCargadoAction, EspecialCargadoSuccessCheck);
        DecisionInspirar = BT.CreateSelectorNode("DecisionInspirar");
        ComprobarInspirar = BT.CreateSequenceNode("ComprobarInspirar", false);
        AliadoARango = BT.CreateLeafNode("AliadoARango", AliadoARangoAction, AliadoARangoSuccessCheck);
        Inspirar = BT.CreateLeafNode("Inspirar", InspirarAction, InspirarSuccessCheck);
        MoverAAliado = BT.CreateLeafNode("MoverAAliado", MoverAAliadoAction, MoverAAliadoSuccessCheck);
        DecisionMoverse = BT.CreateSelectorNode("DecisionMoverse");
        SecTorre = BT.CreateSequenceNode("SecTorre", false);
        TorreAsequible = BT.CreateLeafNode("TorreAsequible", TorreAsequibleAction, TorreAsequibleSuccessCheck);
        TorreVacia = BT.CreateLeafNode("TorreVacia", TorreVaciaAction, TorreVaciaSuccessCheck);
        SubirTorre = BT.CreateLeafNode("SubirTorre", SubirTorreAction, SubirTorreSuccessCheck);
        MoverseEnemigo = BT.CreateLeafNode("MoverseEnemigo", MoverseAction, MoverseSuccessCheck);

        LoopDecoratorNode mainLoop = BT.CreateLoopNode("loop", RootSelector);

        // Child adding
        RootSelector.AddChild(ProvocandoSec);
        RootSelector.AddChild(EnemigoARangoSec);
        RootSelector.AddChild(DecisionMovimiento);

        ProvocandoSec.AddChild(EnemigoProvocando);
        ProvocandoSec.AddChild(DecisionProvocando);

        DecisionProvocando.AddChild(SecuenciaProvocando);
        DecisionProvocando.AddChild(Moverse);

        SecuenciaProvocando.AddChild(EnemigoRango);
        SecuenciaProvocando.AddChild(AtacarProvocado);

        EnemigoARangoSec.AddChild(EnemigoRangoAtaque);
        EnemigoARangoSec.AddChild(SelectorAtaque);

        SelectorAtaque.AddChild(SecVidaAtaque);
        SelectorAtaque.AddChild(DecisionPocaVida);

        SecVidaAtaque.AddChild(SuficienteVida);
        SecVidaAtaque.AddChild(Atacar);

        DecisionPocaVida.AddChild(SecPocaVida);
        DecisionPocaVida.AddChild(Auxilio);

        SecPocaVida.AddChild(EnemigoPocaVida);
        SecPocaVida.AddChild(AtacarPocaVida);

        DecisionMovimiento.AddChild(SecVida);
        DecisionMovimiento.AddChild(SecInspirar);
        DecisionMovimiento.AddChild(MoverseEnemigo);

        SecVida.AddChild(NoSuficienteVida);
        SecVida.AddChild(DecisionObjVida);

        DecisionObjVida.AddChild(SecVidaGenerada);
        DecisionObjVida.AddChild(AuxilioNoOBjVida);

        SecVidaGenerada.AddChild(VidaGenerada);
        SecVidaGenerada.AddChild(MoverseVida);

        SecInspirar.AddChild(EspecialCargado);
        SecInspirar.AddChild(DecisionInspirar);

        DecisionInspirar.AddChild(ComprobarInspirar);
        DecisionInspirar.AddChild(MoverAAliado);

        ComprobarInspirar.AddChild(AliadoARango);
        ComprobarInspirar.AddChild(Inspirar);

        //DecisionMoverse.AddChild(SecTorre);
        //DecisionMoverse.AddChild(MoverseEnemigo);

        //SecTorre.AddChild(TorreAsequible);
        //SecTorre.AddChild(TorreVacia);
        //SecTorre.AddChild(SubirTorre);

        // SetRoot
        BT.SetRootNode(RootSelector);

    }


    /* ACCIONES */

    #region Provocando Secuencia
    private void EnemigoProvocandoAction()
    {

    }

    private ReturnValues EnemigoProvocandoSuccessCheck()
    {
        if (GetComponent<RangerController>().AlguienProvocando())
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }
    #endregion

    #region Inspirar Secuencia
    private void EspecialCargadoAction()
    {

    }

    private ReturnValues EspecialCargadoSuccessCheck()
    {
        if (GetComponent<RangerController>().tengoAtaqueEspecial())
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void AliadoARangoAction()
    {
        
    }

    private ReturnValues AliadoARangoSuccessCheck()
    {
        var aliado = GetComponent<RangerController>().GetPersonaje() as Ranger;
        this.aliadosCerca = aliado.AliadoARango();
        if (aliadosCerca)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void InspirarAction()
    {
        GetComponent<RangerController>().Inspirar();

        GetComponent<RangerController>().FinTurno();
        GetBT().Active = false;
    }

    private ReturnValues InspirarSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "Curo"));
        return ReturnValues.Succeed;
    }

    private void MoverAAliadoAction()
    {
        var aliado = GetComponent<RangerController>().getAliadoCercano();
        GetComponent<RangerController>().Moverse(aliado.transform.position);

        GetComponent<RangerController>().FinTurno();
    }

    private ReturnValues MoverAAliadoSuccessCheck()
    {
        var aliado = GetComponent<RangerController>().GetPersonaje() as Ranger;
        this.aliadosCerca = aliado.AliadoARango();
        if (aliadosCerca)
        {
            return ReturnValues.Failed;
        }
        else
        {
            StartCoroutine(muestraBocadillo(true, "Voy a Aliado"));
            return ReturnValues.Succeed;
        }
    }
    #endregion

    #region Atacar Secuencia
    private void EnemigoRangoAction()
    {

    }

    private ReturnValues EnemigoRangoSuccessCheck()
    {
        enemigo = GetComponent<RangerController>().GetPersonaje().EnemigoARango();

        if (enemigo != null)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void SuficienteVidaAction()
    {

    }

    private ReturnValues SuficienteVidaSuccessCheck()
    {
        if (GetComponent<RangerController>().GetPersonaje().GetVida() > 40)
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
        Debug.Log(gameObject.name + " ataca");
        int vidaEnemigo = GetComponent<RangerController>().GetPersonaje().Atacar(enemigo);
        Debug.Log(enemigo.name + " tiene vida: " + enemigo.GetPersonaje().GetVida());

        if (vidaEnemigo == 0)
        {
            enemigo.GetComponent<PersonajeController>().EstoyMuerto();
        }

        GetComponent<RangerController>().FinTurno();
        this.GetBT().Active = false;
    }

    private ReturnValues AtacarSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "Ataque"));
        return ReturnValues.Succeed;
    }

    private void EnemigoPocaVidaAction()
    {

    }

    private ReturnValues EnemigoPocaVidaSuccessCheck()
    {
        if (enemigo.GetComponent<PersonajeController>().GetPersonaje().GetVida() <
             GetComponent<RangerController>().GetPersonaje().GetVida())
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void AuxilioAction()
    {
        Debug.Log("Socorro, que me pegan");

        var aliado = GetComponent<RangerController>().getAliadoCercano();
        GetComponent<RangerController>().Moverse(aliado.transform.position);
        GetComponent<RangerController>().pidiendoAuxilio();
        //GetComponent<PersonajeController>().FinTurno();
        this.GetBT().Active = false;
    }

    private ReturnValues AuxilioSuccessCheck()
    {   
        if (!GetComponent<RangerController>().pidoAuxilio())
        {
            StartCoroutine(muestraBocadillo(true, "Auxilio"));
            Debug.Log(gameObject.name + " Pido auxilio");
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }
    #endregion

    #region Moverse Secuencia
    private void MoverseAction()
    {
        var enemigo = GetComponent<RangerController>().getEnemigoObjetivo();

        GetComponent<RangerController>().Moverse(enemigo.transform.position);



        GetComponent<RangerController>().FinTurno();
        this.GetBT().Active = false;
    }

    private ReturnValues MoverseSuccessCheck()
    {
        enemigo = GetComponent<RangerController>().GetPersonaje().EnemigoARango();

        if (enemigo != null)
        {
            return ReturnValues.Failed;
        }
        else
        {
            StartCoroutine(muestraBocadillo(true, "Voy a Enemigo"));
            return ReturnValues.Succeed;
        }
    }

    private void NoSuficienteVidaAction()
    {

    }

    private ReturnValues NoSuficienteVidaSuccessCheck()
    {
        if (GetComponent<RangerController>().GetPersonaje().GetVida() <= 40)
        {
            return ReturnValues.Succeed;
        }
        else
        {
            return ReturnValues.Failed;
        }
    }

    private void VidaGeneradaAction()
    {

    }

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

    private void MoverseVidaAction()
    {
        var movimientoManager = GetComponent<RangerController>();

        movimientoManager.Moverse(GameManager.objetivo);

        GetComponent<RangerController>().FinTurno();
        this.GetBT().Active = false;
    }

    private ReturnValues MoverseVidaSuccessCheck()
    {
        StartCoroutine(muestraBocadillo(true, "Voy a Vida"));
        return ReturnValues.Succeed;
    }
    #endregion

    private void TorreAsequibleAction()
    {

    }

    private ReturnValues TorreAsequibleSuccessCheck()
    {
        //Write here the code for the success check for TorreAsequible
        return ReturnValues.Failed;
    }

    private void TorreVaciaAction()
    {

    }

    private ReturnValues TorreVaciaSuccessCheck()
    {
        //Write here the code for the success check for TorreVacia
        return ReturnValues.Failed;
    }

    private void SubirTorreAction()
    {

    }

    private ReturnValues SubirTorreSuccessCheck()
    {
        //Write here the code for the success check for SubirTorre
        return ReturnValues.Failed;
    }
}
