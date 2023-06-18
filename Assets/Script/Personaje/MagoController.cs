using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagoController : PersonajeController
{
    bool aliadoProvocando = false;

    [SerializeField] private int VidaPoner;

    protected override void Awake()
    {
        this.personaje = new Mago(VidaPoner, 10, "", 5);
        this.personaje.SetVida(VidaPoner);

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
        Debug.Log(aliado.gameObject.name + " está siendo curado");
        Debug.Log("Vida de :" + aliado.gameObject.name + " es: " + aliado.GetPersonaje().GetVida());
        var mago = personaje as Mago;

        aliado.GetPersonaje().SetVida(aliado.GetPersonaje().GetVida() + mago.GetCuracion());
        Mathf.Clamp(aliado.GetPersonaje().GetVida(), 0, 100);
        Debug.Log("Vida de :" + aliado.gameObject.name + " tras curar es: " + aliado.GetPersonaje().GetVida());

        especialCargado = false;
        cargarEspecial();
    }
}
