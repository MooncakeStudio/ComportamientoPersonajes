using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : PersonajeController
{
    #region Atributos
    bool provocando = false;
    bool aliadoProvocando = false;
    #endregion

    #region Delegados
    public delegate void Provocando(GameObject sender);
    public static event Provocando provocandoEvent;
    [SerializeField] private int VidaPoner;
    #endregion

    #region Metodos
    protected override void Awake()
    {
        this.personaje = new Mele(VidaPoner, 10, "");
        GetComponent<BTMele>().GetBT().Active = false;
    }

    protected override void Start() { base.Start(); }

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
    #endregion
}
