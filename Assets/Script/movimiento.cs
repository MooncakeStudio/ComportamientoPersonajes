using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class movimiento : MonoBehaviour
{

    [SerializeField] Sprite spriteDrcha;
    [SerializeField] Sprite spriteIzqda;

    private BehaviourTreeEngine BTPersonaje;

    public GameObject objetivo;

    public void Update()
    {
    }

    private void CrearIA()
    {
        PushPerception hayObj = BTPersonaje.CreatePerception<PushPerception>();


    }

    public void Moverse()
    {
        
    }
}
