using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enumerado que contiene los diferentes patrones de comportamiento de los enemigos
/// </summary>
public enum Comportamiento
{
    /// <summary>
    /// El enemigo hace una ruta aleatoria
    /// </summary>
    Aleatorio,
    /// <summary>
    /// El enemigo hace una ruta aleatoria en la que da un numero de pasos fijos antes de cambiar de dirección
    /// </summary>
    SemiAleatorio,
    /// <summary>
    /// El enemigo hace una ruta aleatoria pero si el jugador esta cerca el enemigo lo comienza a seguir
    /// </summary>
    SeguirJugador
}
/// <summary>
/// Clase que gestiona todas las variables y funciones que del enemigo.
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que el elemento también  tenga asociado
/// <list type="bullet">
/// <item><see cref="Rigidbody2D"/></item>
/// <item><see cref="Animator"/></item>
/// <item><see cref="AudioSource"/></item>
/// <item><see cref="SpriteRenderer"/></item>
/// </list>
/// Además en la escena en la que se use esta clase es necesario que exista un objeto denominado Estadisticas que contenga la clase <see cref="Estadisticas"/> y otro objeto denominado Jugador
/// </remarks>
public class Enemigo : MonoBehaviour
{
    /// <summary>
    /// Dirección en la que aparece el enemigo al comenzar la partida si se establece en undefine se selecciona una dirección aleatoria
    /// </summary>
    public Direccion direccion=Direccion.undefine;
    /// <summary>
    /// Define el comportamiento del enemigo(Seguir, movimiento aleatorio ...)
    /// </summary>
    public Comportamiento comportamiento;
    /// <summary>
    /// Rigidbody del objeto que queramos que sea el enemigo
    /// </summary>
    public Rigidbody2D rbd;
    /// <summary>
    /// Distancia que avanza el enemigo cada vez que se ejecute <see cref="Enemigo.movimiento"/>
    /// </summary>
    public float avance=1f;
    /// <summary>
    /// Frecuencia con la que el enemigo realiza una acción (<see cref="Enemigo.movimiento"/>,<see cref="Enemigo.lanzarProyectil"/>)
    /// </summary>
    public float frecuencia=1f;
    /// <summary>
    /// Daño que realiza el enemigo al golpear al jugador
    /// </summary>
    public float daño = 1f;
    /// <summary>
    /// Vida del enemigo
    /// </summary>
    public float vida=1f;
    /// <summary>
    /// Distancia a la que el enemigo comienza a seguir al jugador si el <see cref="comportamiento"/> está definido como <see cref="Comportamiento.SeguirJugador"/>
    /// </summary>
    public float vision=1f;
    /// <summary>
    /// Tolerancia para considerar que el enemigo se encuentra ya en la posición del jugador
    /// </summary>
    public float tolerancia = 0.5f;
    /// <summary>
    /// Tiempo que tiene que pasar para que el enemigo pueda volver a lanzar el proyectil
    /// </summary>
    public float reutilizacionProyectil=5f;
    /// <summary>
    /// Si el enemigo es empujado al recibir un golpe del arma del jugador (<see cref="DamageJugador"/>)
    /// </summary>
    public bool retrocesoAlDañar = true;
    /// <summary>
    /// Establece la fuerza del empuje que recibe el enemigo
    /// </summary>
    public float magnituRetroceso = 20f;
    /// <summary>
    /// Timer para el proyectil
    /// </summary>
    float tiemerProyectil;
    /// <summary>
    /// Daño que causan al jugador los proyectiles
    /// </summary>
    public float dañoProyectil = 1f;
    /// <summary>
    /// Establece si el enemigo lanza o no proyectiles
    /// </summary>
    public bool lanzaProyectiles=false;
    /// <summary>
    /// Establece si el enemigo tiene animación al lanzar el proyectil o no
    /// </summary>
    public bool animacionLanzar=false;
    /// <summary>
    /// Objeto que invoca el enemigo al lanzar un proyectil este objeto debe poseer el script <see cref="ProyectilEnemigo"/>
    /// </summary>
    public GameObject proyectil;
    /// <summary>
    /// Objeto que contenga la clase <see cref="Jugador"/> en la escena
    /// </summary>
    public GameObject jugador;
    /// <summary>
    /// Timer para la frecuencia
    /// </summary>
    float tiempo;
    /// <summary>
    /// Booleana que indica si el enemigo está lanzando un proyectil.
    /// </summary>
    bool lanzar =false;
    /// <summary>
    /// Color normal del enemigo
    /// </summary>
    Color normalColor;
    /// <summary>
    /// Lista de objetos que pueden aparecer cuando se destruye el arbusto 
    /// </summary>
    public List<GameObject> botin;
    /// <summary>
    /// Probabilidad de que aparezcan objetos de la lista <see cref="botin"/> al destruir el arbusto 0 es que nunca aparecen y 1 siempre aparece un objeto.
    /// </summary>
    public float probBotin = 0.5f; //Entre 0 y 1
    //int direccion = -1;
    /// <summary>
    /// Auxiliar para el movimiento semi aleatorio
    /// </summary>
    int count = 0;
    /// <summary>
    /// Define el número de pasos que da el enemigo antes de cambiar de dirección en si su <see cref="comportamiento"/> es <see cref="Comportamiento.SemiAleatorio"/>
    /// </summary>
    public int pasos=5;
    /// <summary>
    /// Booleana que indica si el enemigo esta vivo.
    /// </summary>
    bool vivo =true;
    /// <summary>
    /// Animator que gestiona las animaciones del elemento que contiene esta clase.
    /// </summary>
    Animator animator;
    /// <summary>
    /// Objeto que se invoca cuando la vida del enemigo es igual o inferior a cero (Efecto de muerte)
    /// </summary>
    public GameObject muerte;
    /// <summary>
    /// Estadísticas de la partida actual
    /// </summary>
    Estadisticas estadisticas = null;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esta habilitado por primera vez antes de update.
    /// En ella se inicializan las variables.
    /// </summary>
    void Start()
    {
        estadisticas = GameObject.Find("Estadisticas").GetComponent<Estadisticas>();
        normalColor = GetComponent<SpriteRenderer>().color;
        tiempo =Time.time;
        tiemerProyectil = Time.time;
        if(jugador== null)
        {
            jugador = GameObject.Find("Jugador");
        }
        animator = GetComponent<Animator>();
        if (comportamiento == Comportamiento.Aleatorio)
        {
            pasos= Random.Range(1, 6);
        }
        switch (direccion)
        {
            case Direccion.derecha:
                animator.SetFloat("Horizontal", Vector2.right.x);
                animator.SetFloat("Vertical", Vector2.right.y);
                break;
            case Direccion.izquierda:
                animator.SetFloat("Horizontal", Vector2.left.x);
                animator.SetFloat("Vertical", Vector2.left.y);
                break;
            case Direccion.arriba:
                animator.SetFloat("Horizontal", Vector2.up.x);
                animator.SetFloat("Vertical", Vector2.up.y);
                break;
            case Direccion.abajo:
                animator.SetFloat("Horizontal", Vector2.down.x);
                animator.SetFloat("Vertical", Vector2.down.y);
                break;
        }
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esté habilitado. 
    /// </summary>
    void Update()
    {
        //rbd.velocity = Vector3.zero;
    }
    /// <summary>
    /// Función que se llama cada cierto numero de frames mientras que el elemento que posee esta clase esté habilitado. 
    /// </summary>
    void FixedUpdate()
    {
        if (Time.time - tiempo > frecuencia && vivo)
        {
            //rbd.velocity = Vector3.zero;
            GetComponent<SpriteRenderer>().color = normalColor;
            tiempo = Time.time;
            //if (mov == false)
            //{
                if ((!lanzaProyectiles || direccion==Direccion.undefine || Time.time - tiemerProyectil < reutilizacionProyectil) && !lanzar) { 
                    //mov = true;
                    switch (comportamiento)
                    {
                        case Comportamiento.Aleatorio:
                            SeleccionarDireccionAleatorio();
                            break;
                        case Comportamiento.SemiAleatorio:
                            SeleccionarDireccionSemiAleatorio();
                            break;
                        case Comportamiento.SeguirJugador:
                            SeleccionarSeguirJugador();
                            break;
                            
                    }
                    movimiento();
                }
                else
                {
                    if (!animacionLanzar)
                    {
                        lanzarProyectil();
                        tiemerProyectil = Time.time;
                    }
                    else
                    {
                        if (!animator.GetBool("Lanzar")) {
                            animator.SetBool("Movimiento", false);
                            animator.SetBool("Lanzar", true);
                            lanzar = true;
                        }
                        else
                        {
                            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                            {
                                lanzar = false;
                                animator.SetBool("Lanzar", false);
                                lanzarProyectil();
                                tiemerProyectil = Time.time;
                            }
                        }
                    }
                }
            //}
        }
    }
    /// <summary>
    /// Función que gestiona el lanzamiento de un proyectil por parte del enemigo
    /// </summary>
    void lanzarProyectil()
    {
        float width = GetComponent<BoxCollider2D>().size.x;
        float height = GetComponent<BoxCollider2D>().size.y;
        switch (direccion)
        {
            case Direccion.arriba:
                Instantiate(proyectil, new Vector3(transform.position.x, transform.position.y + height , 0), Quaternion.identity).GetComponent<ProyectilEnemigo>().establecerValores(dañoProyectil, direccion, this.gameObject);
                break;
            case Direccion.abajo:
                Instantiate(proyectil, new Vector3(transform.position.x, transform.position.y - height , 0), Quaternion.identity).GetComponent<ProyectilEnemigo>().establecerValores(dañoProyectil, direccion,this.gameObject);
                break;
            case Direccion.derecha:
                Instantiate(proyectil, new Vector3(transform.position.x + width, transform.position.y, 0), Quaternion.identity).GetComponent<ProyectilEnemigo>().establecerValores(dañoProyectil, direccion, this.gameObject);
                break;
            case Direccion.izquierda:
                Instantiate(proyectil, new Vector3(transform.position.x - width , transform.position.y, 0), Quaternion.identity).GetComponent<ProyectilEnemigo>().establecerValores(dañoProyectil, direccion, this.gameObject);
                break;
        }
    }
    /// <summary>
    /// Función que selecciona una nueva dirección de movimiento si se han dado ya tantos pasos en la dirección anterior
    /// como se indica en la variable pasos
    /// </summary>
    void SeleccionarDireccionSemiAleatorio()
    {
        count++;
        if (direccion == Direccion.undefine || count > pasos)
        {
            direccion = (Direccion)Random.Range(0, 4);
            count = 0;
        }
    }
    /// <summary>
    /// Función que selecciona una nueva dirección de movimiento
    /// </summary>
    void SeleccionarDireccionAleatorio()
    {
        count++;
        if (direccion == Direccion.undefine || count > pasos)
        {
            direccion = (Direccion)Random.Range(0, 4);
            count = 0;
            pasos = Random.Range(1, 6);
        }
    }
    /// <summary>
    /// Función que establece la dirección en arriba o abajo en función de un parámetro
    /// </summary>
    /// <param name="y">Parámetro que se usa como referencia para la selección de dirección</param>
    void SeleccionarSeguirJugadorY(float y)
    {
        if (y > 0)
        {
            direccion = Direccion.arriba;
        }
        else
        {
            direccion = Direccion.abajo;
        }
    }
    /// <summary>
    /// Función que establece la dirección en derecha o izquierda en función de un parámetro
    /// </summary>
    /// <param name="x">Parámetro que se usa como referencia para la selección de dirección</param>
    void SeleccionarSeguirJugadorX(float x)
    {
        if (x > 0)
        {
            direccion = Direccion.derecha;
        }
        else
        {
            direccion = Direccion.izquierda;
        }
    }
    /// <summary>
    /// Función que se encarga de seleccionar una dirección para aproximar al enemigo al jugador
    /// </summary>
    void SeleccionarSeguirJugador()
    {
        if (Vector2.Distance(transform.position, jugador.GetComponent<Transform>().position) <= vision)
        {
            //Debug.Log("Te veo");
            //direccion = Direccion.undefine;
            float difereciaX = jugador.GetComponent<Transform>().position.x- transform.position.x;
            float difereciaY = jugador.GetComponent<Transform>().position.y- transform.position.y;
            //Debug.Log("X : " + difereciaX);
            //Debug.Log("Y : " + difereciaY);
            if ((difereciaX > tolerancia || difereciaX < -tolerancia) && (difereciaY > tolerancia || difereciaY < -tolerancia))
            {
                //Debug.Log("Los dos diferentes de 0");
                switch (Random.Range(0, 2))
                {
                    case 0:
                           SeleccionarSeguirJugadorX(difereciaX);
                        
                        break;
                    case 1:          
                           SeleccionarSeguirJugadorY(difereciaY);
                       break;
                }
            }
            else
            {
                if (difereciaX > tolerancia || difereciaX < -tolerancia )
                {
                    SeleccionarSeguirJugadorX(difereciaX);
                    //Debug.Log("Diferente Eje X");
                    
                }
                else
                {
                    SeleccionarSeguirJugadorY(difereciaY);
                    //Debug.Log("Diferente Eje Y");
                }
            }
        }
        else
        {
            SeleccionarDireccionAleatorio();
        }
    }
    /// <summary>
    /// Gestiona el movimiento del enemigo en función de su dirección
    /// </summary>
    void movimiento()
    {
        animator.SetBool("Movimiento", true);
        //count++;
        //if (direccion == Direccion.undefine || count > pasos)
        //{
        //    direccion = (Direccion)Random.Range(0, 4);
        //    count = 0;
        //}
        switch (direccion)
        {
            case Direccion.derecha:
                transform.Translate(Vector2.right * avance * Time.deltaTime);
                animator.SetFloat("Horizontal", Vector2.right.x);
                animator.SetFloat("Vertical", Vector2.right.y);
                break;
            case Direccion.izquierda:
                transform.Translate(Vector2.left * avance * Time.deltaTime);
                animator.SetFloat("Horizontal", Vector2.left.x);
                animator.SetFloat("Vertical", Vector2.left.y);
                break;
            case Direccion.arriba:
                transform.Translate(Vector2.up * avance * Time.deltaTime);
                animator.SetFloat("Horizontal", Vector2.up.x);
                animator.SetFloat("Vertical", Vector2.up.y);
                break;
            case Direccion.abajo:
                transform.Translate(Vector2.down * avance * Time.deltaTime);
                animator.SetFloat("Horizontal", Vector2.down.x);
                animator.SetFloat("Vertical", Vector2.down.y);
                break;
        }
        //mov = false;
    }
    /// <summary>
    /// Función que se encarga de gestionar la perdida de vida del enemigo
    /// </summary>
    /// <param name="daño">Cantidad de vida que pierde el enemigo</param>
    void damage(float daño)
    {
        vida = vida - daño;
    }
    /// <summary>
    /// Agrega al objeto del enemigo una fuerza equivalente al vector normalizado de la diferencia del vector del jugador con el vector de la colisión
    /// </summary>
    /// <param name="collision">Colision que se usa de referencia para calcular el vector del empuje</param>
    void empujar(Collision2D collision)
    {
        Vector3 force = transform.position - collision.transform.position;
        force.Normalize();
        rbd.AddForce(force * magnituRetroceso);
    }
    /// <summary>
    /// Función que se lanza cuando el objeto entra colisión
    /// </summary>
    /// <param name="collision">Objeto generado de por la colisión</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Jugador")
        {
            //Debug.Log("Colision con jugador");
        }
        if (collision.gameObject.tag == "DañoJugador")
        {
            //Debug.Log("Colision con arma");
            //animator.SetBool("Movimiento", false);
            GetComponent<SpriteRenderer>().color = Color.red;
            damage(collision.gameObject.GetComponent<DamageJugador>().ataque);
            if (vida <= 0)
            {
                if (estadisticas != null)
                {
                    estadisticas.enemigosDerrotados++;
                }
                vivo = false;
                if (botin.Count > 0)
                {
                    if (Random.Range(0f, 1f) <= probBotin)
                    {
                        Instantiate(botin[Random.Range(0, botin.Count)], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                }
                Instantiate(muerte, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                Destroy(this.gameObject);
            }
            empujar(collision);
            //var magnitude = 10;
            //Vector3 force = transform.position - collision.transform.position;
            //force.Normalize();
            //rbd.AddForce(force * magnitude);
        }
    }
}
