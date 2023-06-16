using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoController : PersonajeController
{
    bool aliadoProvocando = false;

    [SerializeField] private int VidaPoner;

    protected override void Awake()
    {
        this.personaje = new Mago(VidaPoner, 10, "",5);

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

    public void Curar(PersonajeController aliado)
    {
        var mago = personaje as Mago;
        aliado.GetPersonaje().CurarVida(mago.GetCuracion());
    }
}
