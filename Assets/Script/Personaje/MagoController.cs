using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoController : PersonajeController
{
    bool aliadoProvocando = false;

    //Delegados
    public delegate void Provocando(GameObject sender);
    public static event Provocando provocandoEvent;
    [SerializeField] private int VidaPoner;

    protected override void Awake()
    {
        this.personaje = new Mago(VidaPoner, 10, "");

        GetComponent<BTMago>().GetBT().Active = false;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public bool aliadoEstaProvocando() { return aliadoProvocando; }
}
