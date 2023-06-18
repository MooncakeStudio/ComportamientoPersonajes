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
    [SerializeField] private int ataque;

    protected override void Awake()
    {
        this.personaje = new Mele(VidaPoner,ataque,"");
        this.personaje.SetVida(VidaPoner);
        personaje.SetAtaque(ataque);

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
        GetComponent<Animator>().SetTrigger("Especial");
        provocandoEvent?.Invoke(gameObject);
    }
}
