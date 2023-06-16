using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    #region Atributos y Getters-Setters
    private Objeto objeto;

    public Objeto GetObjeto() { return this.objeto; }
    public void SetObjeto(Objeto objeto) { this.objeto = objeto; }
    #endregion

    #region Metodos
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Equipo1") || collision.gameObject.CompareTag("Equipo2"))
        {
            var personaje = collision.gameObject.GetComponent<PersonajeController>().GetPersonaje(); // as PersonajeController;
            //var personaje = personajeController.GetPersonaje();

            personaje.SetVida(personaje.GetVida() + objeto.GetVida());
            collision.gameObject.GetComponent<PersonajeController>().SetPersonaje(personaje);

            if (personaje.GetVida() > 20)
            {
                collision.gameObject.GetComponent<PersonajeController>().noMasAuxilio();
            }
            Debug.Log("Mira mi vida: " + personaje.GetVida());

            Destroy(this.gameObject);
        }
    }

    public void OnDestroy() { GameManager.hayObj = false; }
    #endregion
}
