using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    [SerializeField] public List<GameObject> ejercito;
    [SerializeField] public bool esTurno;

    public void MoverEjercito()
    {
         foreach (var soldado in ejercito)
         {
            soldado.GetComponent<movimiento>().esTurno = true;
            //soldado.GetComponent<movimiento>().realizaAccion();
         }
    }
}
