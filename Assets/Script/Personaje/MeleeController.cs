using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : PersonajeController
{

    bool provocando = false;

    //Delegados
    public delegate void Provocando(GameObject sender);
    public static event Provocando provocandoEvent;
    [SerializeField] private int VidaPoner;
    [SerializeField] private int ataque;

    protected override void Awake()
    {
        this.personaje = new Mele();
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

    public void Provocar()
    {
        provocando = true;
        especialCargado=false;
        cargarEspecial();
        provocandoEvent?.Invoke(gameObject);
    }
}
