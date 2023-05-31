using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : PersonajeController
{

    bool provocando = false;
    bool aliadoProvocando = false;

    protected override void Awake()
    {
        this.personaje = new Mele(10,10,"");

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
    }
}
