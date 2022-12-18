using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{

    bool provocando = false;
    bool aliadoProvocando = false;


    public bool aliadoEstaProvocando() { return aliadoProvocando; }

    public void Provocar()
    {
        provocando= true;
    }
}
