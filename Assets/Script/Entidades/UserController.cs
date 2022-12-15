using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UserController : MonoBehaviour
{
    [SerializeField] public List<GameObject> ejercito;
    [SerializeField] public bool esTurno;

    public void MoverEjercito()
    {
         foreach (var soldado in ejercito)
         {
            soldado.GetComponent<BTMele>().esTurno = true;
            //soldado.GetComponent<movimiento>().realizaAccion();
         }
    }

    public IEnumerator MoverSoldados(int contador, int maxSoldados)
    {
        ejercito[contador].GetComponent<BTMele>().esTurno = true;
        contador++;

        yield return new WaitForSeconds(1);

        if(contador < maxSoldados)
        {
            MoverSoldados(contador, maxSoldados);
        }
    }
}
