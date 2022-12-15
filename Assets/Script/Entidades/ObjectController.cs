using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public void OnDestroy()
    {
        GameManager.hayObj = false;
    }
}
