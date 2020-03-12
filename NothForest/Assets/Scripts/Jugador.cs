using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enumerado que contiene todas las posibilidades de dirección de los enemigos y el jugador
/// </summary>
public enum Direccion
{
    /// <summary>
    /// Dirección izquierda
    /// </summary>
    izquierda,
    /// <summary>
    /// Dirección arriba
    /// </summary>
    arriba,
    /// <summary>
    /// Dirección abajo
    /// </summary>
    abajo,
    /// <summary>
    /// Dirección derecha
    /// </summary>
    derecha,
    /// <summary>
    /// Dirección no especificada
    /// </summary>
    undefine
}
/// <summary>
/// Clase que gestiona todas las variables y funciones que del jugador.
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que el elemento también  tenga asociado
/// <list type="bullet">
/// <item><see cref="Rigidbody2D"/></item>
/// <item><see cref="Animator"/></item>
/// <item><see cref="AudioSource"/></item>
/// <item><see cref="SpriteRenderer"/></item>
/// <item>Algun tipo de colaider 2d por ejemplo <see cref="CapsuleCollider2D"/></item>
/// </list>
/// Además en la escena en la que se use esta clase es necesario que exista un objeto denominado Estadisticas y que contenga la clase <see cref="Estadisticas"/>
/// </remarks>
public class Jugador : MonoBehaviour
{
    /// <summary>
    /// Distancia a la que aparece el <see cref="arma"/> del jugador
    /// </summary>
    float separacionDamageObject = 0f;
    /// <summary>
    /// Rigidbody2D del jugador
    /// </summary>
    public Rigidbody2D rbd;
    /// <summary>
    /// Velocidad del movimiento del jugador
    /// </summary>
    public float velocidad = 5f;
    /// <summary>
    /// Animator que gestiona las animaciones del elemento que contiene esta clase.
    /// </summary>
    public Animator animator;
    /// <summary>
    /// Tiempo que es imune al daño el jugador tras recibir daño
    /// </summary>
    public float temporalImune = 1f;
    /// <summary>
    /// Durancion de la animación de ataque del jugador
    /// </summary>
    public float duracionAnimacionAtaque=0.3f;
    /// <summary>
    /// Fuerza del empuje que se aplica al jugador tras colisionar con un enemigo
    /// </summary>
    public float magnituRetroceso = 20f;
    /// <summary>
    /// Tiempo que el jugador en poder moverse y atacar mientras dure el empuje 
    /// </summary>
    public float tiempoRetroceso=1f;
    /// <summary>
    /// Tiempo que el jugador es inmune a recibir un empuje después de recibir uno
    /// </summary>
    public float cdEntreRetrocesos=0.5f;
    /// <summary>
    /// Boleana que indica si el jugador está siendo empujado
    /// </summary>
    bool empuje = false;
    /// <summary>
    /// Direccion a la que está mirando el jugador
    /// </summary>
    Direccion direccion=Direccion.abajo;
    /// <summary>
    /// Temporizador auxiliar para el imune al daño
    /// </summary>
    float temporizador =0;//Temporizador auxiliar para el imune
    /// <summary>
    /// Temporizador auxiliar para la duración del retroceso
    /// </summary>
    float temporizadorretroceso = 0;//Temporizador auxiliar para el retroceso
    /// <summary>
    /// Temporizador auxiliar para el cdRetrocesos
    /// </summary>
    float temporizadorcdretroceso = 0;//Temporizador auxiliar para el cdRetrocesos
    /// <summary>
    /// Boooleana que indica que el jugador está vivo
    /// </summary>
    bool vivo = true;
    /// <summary>
    /// Cantidad de vida actual del jugador
    /// </summary>
    float vida;
    /// <summary>
    /// Color normal de jugador
    /// </summary>
    Color normalColor;
    /// <summary>
    /// Estadisticas de la partida actual
    /// </summary>
    Estadisticas estadisticas =null;
    /// <summary>
    /// AudioSource del objeto jugador
    /// </summary>
    AudioSource audioSorce;
    /// <summary>
    /// Sonido que se reproduce cuando el jugador ataca
    /// </summary>
    public AudioClip sonidoAtaque;
    /// <summary>
    /// Sonido que se reproduce cuando el jugador recupera vida
    /// </summary>
    public AudioClip sonidoCuracion;
    /// <summary>
    /// Sonido que se reproduce cuando el jugador recibe daño
    /// </summary>
    public AudioClip sonidoDaño;
    /// <summary>
    /// Devuelve el valor de la variable vida
    /// </summary>
    public float Vida
    {
        get { return vida; }
    }
    /// <summary>
    /// Daño que se estableza al <see cref="arma"/> del jugador
    /// </summary>
    public float fuerza=1f;
    /// <summary>
    /// Vida maxima del jugador
    /// </summary>
    public float MaxVida = 3;
    /// <summary>
    /// Booleana que indica si el jugador está atacando
    /// </summary>
    bool atacar = false;
    /// <summary>
    /// Objeto que invoca el jugador al atacar que debe poseer <see cref="DamageJugador"/>
    /// </summary>
    public GameObject arma;
    /// <summary>
    /// Temporizador auxiliar para la duración del ataque
    /// </summary>
    float atacando =0;
    /// <summary>
    /// Vector para el movimiento del jugador
    /// </summary>
    Vector2 vector;
    /// <summary>
    /// Configuracion del juego actual
    /// </summary>
    RegistroConfiguracion configuracion;
    // Start is called before the first frame update
    // Update is called once per frame
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase está habilitado por primera vez antes de update.
    /// En ella se inicializan las variables.
    /// </summary>
    void Start()
    {
        audioSorce = GetComponent<AudioSource>();
        estadisticas = GameObject.Find("Estadisticas").GetComponent<Estadisticas>();
        configuracion= GuardarCargarConf.cargarConfiguracion();
        if (configuracion != null)
        {
            if (!configuracion.sonido)
            {
                audioSorce.mute = true;
            }
        }
        vida = MaxVida;
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", -1);
        normalColor = GetComponent<SpriteRenderer>().color;
        temporizador =- temporalImune;
    }
    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esté habilitado. 
    /// </summary>
    void Update()
    {
        //rbd.velocity = Vector3.zero;
        if (Time.time - temporizador > temporalImune)
        {
            GetComponent<SpriteRenderer>().color = normalColor;
        }
        if (!empuje)
        {
            vector.x = Input.GetAxis("Horizontal");
            vector.y = Input.GetAxis("Vertical");
        }
        else
        {
            vector.x = 0;
            vector.y = 0;
        }
        if (atacar && atacando==0)
        {
            atacando = Time.time;
        }
        if (Time.time-atacando> duracionAnimacionAtaque)
        {
            animator.SetBool("Atacar", false);
            atacar = false;
            atacando = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !atacar)
        {
            ataque();
        }
        if (empuje)
        {
            //Debug.Log(Time.time - temporizadorretroceso > tiempoRetroceso);
            if (Time.time - temporizadorretroceso > tiempoRetroceso)
            {
                rbd.velocity = Vector3.zero;
                rbd.angularVelocity = 0f;
                empuje = false;
                temporizadorcdretroceso = Time.time;
            }
        }
        
        //Debug.Log("Velocidad x:"+rbd.velocity.x + "Velocidad y" + rbd.velocity.y );
        if (vida < 0.1)
        {
            vivo = false;
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }
    /// <summary>
    /// Función que ejecuta el ataque del jugador
    /// </summary>
    public void ataque(){
        if (!atacar && vivo && !empuje/*(Time.time - temporizadorretroceso > tiempoRetroceso ||temporizadorretroceso==0)*/)
        {
            atacar = true;
            animator.SetBool("Atacar", true);
            float width = GetComponent<BoxCollider2D>().size.x;
            float height = GetComponent<BoxCollider2D>().size.y;
            if(estadisticas != null)
            {
                estadisticas.ataquesRealizados++;
            }
            switch (direccion)
            {
                case Direccion.arriba:
                    Instantiate(arma, new Vector3(transform.position.x, transform.position.y + height + separacionDamageObject, 0), Quaternion.identity).GetComponent<DamageJugador>().establecerPropiedades(fuerza, duracionAnimacionAtaque,transform.gameObject);
                    break;
                case Direccion.abajo:
                    Instantiate(arma, new Vector3(transform.position.x, transform.position.y - height - separacionDamageObject, 0), Quaternion.Euler(180f, 180f, 0f)).GetComponent<DamageJugador>().establecerPropiedades(fuerza, duracionAnimacionAtaque, transform.gameObject);
                    break;
                case Direccion.derecha:
                    Instantiate(arma, new Vector3(transform.position.x+width+ separacionDamageObject, transform.position.y, 0), Quaternion.Euler(0f, 0f, -90f)).GetComponent<DamageJugador>().establecerPropiedades(fuerza, duracionAnimacionAtaque, transform.gameObject);
                    break;
                case Direccion.izquierda:
                    Instantiate(arma, new Vector3(transform.position.x - width - separacionDamageObject, transform.position.y, 0), Quaternion.Euler(180f, 0f, 90f)).GetComponent<DamageJugador>().establecerPropiedades(fuerza, duracionAnimacionAtaque, transform.gameObject);
                    break;
            }
            audioSorce.clip = sonidoAtaque;
            audioSorce.Play();
            //atacar = false;
        }
    }
    /// <summary>
    /// Función que se llama cada cierto numero de frames mientras que el elemento que posee esta clase esté habilitado. 
    /// </summary>
    void FixedUpdate()
    {
            mover(vector);
    }
    /// <summary>
    /// Desplaza al jugador en función de un vector
    /// </summary>
    /// <param name="vector">vector que dirige el movimiento</param>
    void mover(Vector2 vector)
    {
        //Debug.Log("Velocidad :"+rbd.velocity);
        if (vivo && !atacar && !empuje)
        {
            if (vector.x != 0)
            {
                if (vector.x > 0)
                {
                    direccion = Direccion.derecha;
                }
                else
                {
                    direccion = Direccion.izquierda;
                }
                vector.y = 0;
                this.vector.y = 0;
                animator.SetBool("Movimiento", true);
                animator.SetFloat("Horizontal", vector.x);
                animator.SetFloat("Vertical", vector.y);
                rbd.MovePosition(rbd.position + vector * velocidad * Time.fixedDeltaTime);
                return;
            }
            if (vector.y != 0)
            {
                if (vector.y > 0)
                {
                    direccion = Direccion.arriba;
                }
                else
                {
                    direccion = Direccion.abajo;
                }
                vector.x = 0;
                this.vector.x = 0;
                animator.SetFloat("Horizontal", vector.x);
                animator.SetFloat("Vertical", vector.y);
                animator.SetBool("Movimiento", true);
                rbd.MovePosition(rbd.position + vector * velocidad * Time.fixedDeltaTime);
                return;
            }
        }
        else
        {
            return;
        }
        animator.SetBool("Movimiento", false);
    }
    /// <summary>
    /// Función que en caso de que  el jugador está vivo desplaza al jugador hacia derecha
    /// </summary>
    /// <remarks>
    /// Vease también :
    /// <seealso cref="mover(Vector2)"/>
    /// <seealso cref="moverAbajo"/>
    /// <seealso cref="moverArriba"/>
    /// <seealso cref="moverIzquierda"/>
    /// </remarks>
    public void moverDerecha()
    {
            Vector2 vec = new Vector2(1, 0);
            mover(vec);
    }
    /// <summary>
    /// Función que en caso de que  el jugador está vivo desplaza al jugador hacia izquierda
    /// </summary>
    /// <remarks>
    /// Vease también :
    /// <seealso cref="mover(Vector2)"/>
    /// <seealso cref="moverAbajo"/>
    /// <seealso cref="moverArriba"/>
    /// <seealso cref="moverDerecha"/>
    /// </remarks>
    public void moverIzquierda()
    {
            Vector2 vec = new Vector2(-1, 0);
            mover(vec);
    }
    /// <summary>
    /// Función que en caso de que  el jugador está vivo desplaza al jugador hacia arriba
    /// </summary>
    /// <remarks>
    /// Vease también :
    /// <seealso cref="mover(Vector2)"/>
    /// <seealso cref="moverAbajo"/>
    /// <seealso cref="moverDerecha"/>
    /// <seealso cref="moverIzquierda"/>
    /// </remarks>
    public void moverArriba()
    {
            Vector2 vec = new Vector2(0, 1);
            mover(vec);
    }
    /// <summary>
    /// Función que en caso de que  el jugador está vivo desplaza al jugador hacia abajo
    /// </summary>
    /// <remarks>
    /// Vease también :
    /// <seealso cref="mover(Vector2)"/>
    /// <seealso cref="moverDerecha"/>
    /// <seealso cref="moverArriba"/>
    /// <seealso cref="moverIzquierda"/>
    /// </remarks>
    public void moverAbajo()
    {
            Vector2 vec = new Vector2(0, -1);
            mover(vec);
    }
    /// <summary>
    /// Función que se encarga de gestionar la pérdida de vida del jugador y el accionamiento de la vibración del dispositivo
    /// </summary>
    /// <param name="damage">Cantidad de vida que pierde el jugador</param>
    void damage(float damage)
    {
        if (Time.time - temporizador > temporalImune)
        {
            audioSorce.clip = sonidoDaño;
            audioSorce.Play();
            if (configuracion.vibracion)
            {
                Handheld.Vibrate();
            }
            GetComponent<SpriteRenderer>().color = Color.red;
            vida = vida - damage;
            temporizador = Time.time;
        }
    }
    /// <summary>
    /// Función que se encarga de gestionar la pérdida de vida del jugador y lo empuja (<see cref="empujar(Collision2D)"/>).
    /// Tambien gestiona la vibración del dispositivo.
    /// </summary>
    /// <param name="damage">Cantidad de vida que pierde el jugador</param>
    ///  <param name="col">Colisión que causo daño al jugador</param>
    void damage(float damage, Collision2D col)
    {
        if (!empuje && !atacar && (Time.time - temporizadorcdretroceso > cdEntreRetrocesos || temporizadorcdretroceso==0))
        {
            empujar(col);
            temporizadorretroceso = Time.time;
        }
        this.damage(damage);
    }
    /// <summary>
    /// Función que se encarga de gestionar la recuperación de vida del jugador
    /// </summary>
    /// <param name="heal">Valor de vida que recupera el jugador</param>
    void heal(float heal)
    {
        if (vida < MaxVida)
        {
            audioSorce.clip = sonidoCuracion;
            audioSorce.Play();
            if (vida + heal > MaxVida)
            {
                vida = MaxVida;
            }
            else
            {
                vida = vida + heal;
            }
        }
    }
    /// <summary>
    /// Agrega al objeto del jugador una fueza equivalente al vector normalizado de la diferencia del vector del jugador con el vector de la colisión
    /// </summary>
    /// <param name="collision">Colision que se usa de referencia para calcular el vector del empuje</param>
    void empujar(Collision2D collision)
    {
        //Debug.Log("Empujar");
        empuje = true;
        Vector3 force = transform.position - collision.transform.position;
        force.Normalize();
        rbd.AddForce(force * magnituRetroceso);
    }
    /// <summary>
    /// Función que se lanza cuando el objeto entra colisión
    /// </summary>
    /// <param name="col">Objeto generado de por la colisión</param>
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("OnCollisionEnter2D");
        if (col.gameObject.tag == "Enemigo" )
        {
            //vida--;
            //Debug.Log("OnCollisionEnter2D");
            //GetComponent<SpriteRenderer>().color = Color.red;
            damage(col.gameObject.GetComponent<Enemigo>().daño,col);
            //temporizador = Time.time;
        }
        if (col.gameObject.tag == "ProyectilEnemigo")
        {
            damage(col.gameObject.GetComponent<ProyectilEnemigo>().Daño);
        }
        if (col.gameObject.tag == "Curacion")
        {
            //Debug.Log(col.gameObject.GetComponent<ObjetoCura>().curacion);
            heal(col.gameObject.GetComponent<ObjetoCura>().curacion);
        }
    }
    /// <summary>
    /// Función que se lanza mientras el objeto permanezca en colisión
    /// </summary>
    /// <param name="col">Objeto generado de por la colisión</param>
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemigo")
        {
            //if (Time.time - temporizador>temporalImune) {
                //GetComponent<SpriteRenderer>().color = Color.red;
                damage(col.gameObject.GetComponent<Enemigo>().daño,col);
                //Debug.Log("MantieneColision");
                //temporizador = Time.time;
            //}
        }
    }
}
