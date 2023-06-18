using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PersonajeController : MonoBehaviour
{
    // ATRIBUTOS

    protected Personaje personaje;

    protected GameObject enemigoObjetivo;
    protected GameObject aliadoCercano;
    protected Celda objetivo;

    [SerializeField] protected bool aliadoAuxilio = false;
    protected bool especialCargado = false;
    protected bool necesitoAuxilio = false;
    [SerializeField] protected bool enemigoProvocando = false;


    [SerializeField] protected float velocidad;
    private UserController usuarioControlador;

    [SerializeField] public Sprite accionImage;
    [SerializeField] public Sprite percepcionImage;

    //Delegado para auxilio
    public delegate void PidiendoAuxilio(GameObject sender);
    public static event PidiendoAuxilio PidiendoAuxilioEvent;

    public delegate void NoPidiendoAxuilio(GameObject sender);
    public static event NoPidiendoAxuilio NoPidiendoAxuilioEvent;

    public delegate void Muerto(GameObject sender);
    public static event Muerto MuertoEvent;
    bool muertoInvocado = false;
    // GETTERS & SETTERS

    public Personaje GetPersonaje() { return personaje; }

    public void SetPersonaje(Personaje personaje) { this.personaje = personaje; }

    public bool alguienPidiendoAuxilio() { return aliadoAuxilio; }
    public bool AlguienProvocando() { return enemigoProvocando; }

    public bool tengoAtaqueEspecial() { return especialCargado; }

    public bool pidoAuxilio() { return necesitoAuxilio; }

    public void pidiendoAuxilio() { necesitoAuxilio = true; FinTurno(); PidiendoAuxilioEvent?.Invoke(gameObject); }
    public void noMasAuxilio() { necesitoAuxilio = false; NoPidiendoAxuilioEvent?.Invoke(gameObject); }

    public void SetEnemigoProvocando(bool enemigoProvocando) { this.enemigoProvocando = enemigoProvocando; }
    public void SetAliadoPidiendoAuxilio(bool aliadoAuxilio) { this.aliadoAuxilio = aliadoAuxilio; }
    public float GetVelocidad() { return velocidad; }

    public void setEnemigoObjetivo(GameObject enemigoObjetivo) { this.enemigoObjetivo = enemigoObjetivo; }

    public void setAliadoCercano(GameObject aliadoCercano) { this.aliadoCercano = aliadoCercano; }
    public GameObject getAliadoCercano() { return aliadoCercano; }
    public GameObject getEnemigoObjetivo() { return enemigoObjetivo; }

    public void FinTurno() { usuarioControlador.turnoFinalizado(); }

    public void Usuario(UserController usuario) { usuarioControlador = usuario; }
    // METODOS

    virtual protected void Awake()
    {
        this.personaje = new Personaje();
    }

    virtual protected void Start()
    {
        personaje.SetFaccion(gameObject.tag);
        StartCoroutine(cargarEspecial());


        int x = Random.Range(0, GameManager.grid.GetAncho());
        int y = Random.Range(0, GameManager.grid.GetAlto());

        while (!GameManager.grid.GetGrid()[x, y].transitable || GameManager.grid.GetGrid()[x, y].isOccupied())
        {
            x = Random.Range(0, GameManager.grid.GetAncho());
            y = Random.Range(0, GameManager.grid.GetAlto());
        }

        personaje.x = x;
        personaje.y = y;

        gameObject.transform.position = GameManager.grid.GetPosicionGlobal(x, y);


        GameManager.grid.GetGrid()[personaje.GetX(), personaje.GetY()].SetPersonaje(GetComponent<PersonajeController>());
    }


    private void Update()
    {
        //if (personaje.GetVida() <= 0)
        //{
        //    Debug.Log("Toy muerto " + gameObject.name);
        //    if (!muertoInvocado)
        //    {
        //        muertoInvocado = true;
        //        MuertoEvent?.Invoke(gameObject);
        //        Destroy(gameObject);

        //    }
        //}
    }

    virtual protected void FixedUpdate()
    {

    }

    public void EstoyMuerto()
    {
        Debug.Log("Toy muerto " + gameObject.name);
        if (!muertoInvocado)
        {
            muertoInvocado = true;
            MuertoEvent?.Invoke(gameObject);
            Destroy(gameObject);

        }
    }

    public void Moverse(Vector3 objetivo)
    {
        GameManager.pf.EncuentraCamino(transform.position, objetivo);
        List<Celda> camino = GameManager.pf.ObtenerCamino(transform.position, objetivo);
        GetComponent<Animator>().SetTrigger("Andar");
        if (camino.Count > 0)
        {
            camino.Reverse();

            this.objetivo = camino[0];
        }

        if (objetivo != null)
        {
            /*while (Vector3.Distance(transform.position, GameManager.grid.GetPosicionGlobal(this.objetivo.xGrid, this.objetivo.yGrid)) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, GameManager.grid.GetPosicionGlobal(this.objetivo.xGrid, this.objetivo.yGrid), 1 * Time.deltaTime);
            }*/

            if (GameManager.grid.GetGrid()[this.objetivo.xGrid, this.objetivo.yGrid].transitable)
            {
                transform.position = GameManager.grid.GetPosicionGlobal(this.objetivo.xGrid, this.objetivo.yGrid);

                GameManager.grid.GetGrid()[personaje.GetX(), personaje.GetY()].SetPersonaje(null);


            int x = this.objetivo.xGrid;
            int y = this.objetivo.yGrid;

            int rotX = x - personaje.x; //Negativo rota
            int rotY = y - personaje.y;//

                personaje.SetX(x);
                personaje.SetY(y);

            //Rotaci�n del personaje en escenario
            if (rotX > 0)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 90, transform.rotation.z));
            if (rotX < 0)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 270, transform.rotation.z));

            if (rotY < 0)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180, transform.rotation.z));
            if (rotY > 0)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, transform.rotation.z));
                
                GameManager.grid.GetGrid()[x, y].SetPersonaje(GetComponent<PersonajeController>());

                this.objetivo = null;
            }
        }
    }

    public IEnumerator cargarEspecial()
    {
        if (!especialCargado)
        {
            yield return new WaitForSeconds(5);
            especialCargado = true;
        }
        //else
        //{

        //}

        //yield return new WaitForSeconds(2);
        //StartCoroutine(cargarEspecial());
    }

    public void BocadilloOn(bool accion, string texto)
    {
        transform.Find("Canvas").transform.Find("TextoBocadillo").GetComponent<TextMeshProUGUI>().text = texto;

        Sprite selectedSprite = accion ? accionImage : percepcionImage;
        transform.Find("Canvas").transform.Find("ImagenBocadillo").GetComponent<Image>().sprite = selectedSprite;

        transform.Find("Canvas").transform.Find("TextoBocadillo").gameObject.SetActive(true);
        transform.Find("Canvas").transform.Find("ImagenBocadillo").gameObject.SetActive(true);
    }

    public void BocadilloOff()
    {
        transform.Find("Canvas").transform.Find("TextoBocadillo").gameObject.SetActive(false);
        transform.Find("Canvas").transform.Find("ImagenBocadillo").gameObject.SetActive(false);
    }
}
