using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class movimiento : MonoBehaviour
{

    [SerializeField] Sprite spriteDrcha;
    [SerializeField] Sprite spriteIzqda;

    public void Moverse()
    {
        var eleccion = UnityEngine.Random.Range(0, 4);

        if (gameObject.transform.position.x >= 90 && eleccion == 0)
        {
            eleccion = UnityEngine.Random.Range(1, 4);
        }
        else if (gameObject.transform.position.x <= -90 && eleccion == 1)
        {
            eleccion = UnityEngine.Random.Range(2, 4);
        }
        else if (gameObject.transform.position.y >= 90 && eleccion == 2)
        {
            eleccion = 3;
        }
        else if (gameObject.transform.position.y <= -90 && eleccion == 3)
        {
            eleccion = UnityEngine.Random.Range(0, 3);
        }


        switch (eleccion)
        {
            case 0:
                Invoke("moverseDerecha", 3);
                break;
            case 1:
                Invoke("moverseIzquierda", 3);
                break;
            case 2:
                Invoke("moverseArriba", 3);
                break;
            case 3:
                Invoke("moverseAbajo", 3);
                break;
            default:
                break;
        }
    }

    private void moverseDerecha()
    {
        var sprite = GetComponent<SpriteRenderer>();

        if(sprite.sprite != spriteDrcha)
        {
            sprite.sprite = spriteDrcha;
        }

        var posicion = gameObject.transform.position;

        posicion.x -= 1;

        gameObject.transform.position = posicion;
    }

    private void moverseIzquierda()
    {
        var sprite = GetComponent<SpriteRenderer>();

        if (sprite.sprite != spriteIzqda)
        {
            sprite.sprite = spriteIzqda;
        }

        var posicion = gameObject.transform.position;

        posicion.x += 1;

        gameObject.transform.position = posicion;
    }

    private void moverseArriba()
    {
        var posicion = gameObject.transform.position;

        posicion.z += 1;

        gameObject.transform.position = posicion;
    }

    private void moverseAbajo()
    {
        var posicion = gameObject.transform.position;

        posicion.z -= 1;

        gameObject.transform.position = posicion;
    }
}
