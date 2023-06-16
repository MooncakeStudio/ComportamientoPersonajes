using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : PersonajeController
{

    bool provocando = false;
    bool aliadoProvocando = false;

    //Delegados
    public delegate void Provocando(GameObject sender);
    public static event Provocando provocandoEvent;
    [SerializeField] private int VidaPoner;

    protected override void Awake()
    {
        this.personaje = new Mele(VidaPoner,10,"");
        this.personaje.SetVida(VidaPoner);

        GetComponent<BTMele>().GetBT().Active = false;
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


    public void Provocar()
    {
        provocando = true;

        provocandoEvent?.Invoke(gameObject);
    }
}
