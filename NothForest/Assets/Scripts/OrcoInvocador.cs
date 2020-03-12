using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que gestiona el funcionamiento del jefe
/// </summary>
/// /// <remarks>
/// Para el funcionamiento de esta clase es necesario que el elemento también  tenga asociado
/// <list type="bullet">
/// <item><see cref="SpriteRenderer"/></item>
/// <item><see cref="Rigidbody2D"/></item>
/// <item><see cref="BoxCollider2D"/></item>
/// <item><see cref="Animator"/></item>
/// </list>
/// Además en la escena en la que se use esta clase es necesario que exista un objeto denominado Estadisticas y que contenga la clase <see cref="Estadisticas"/>
/// </remarks>
public class OrcoInvocador : MonoBehaviour
{
    /// <summary>
    /// Vida máxima del orco invocador
    /// </summary>
    public float vidaMax = 10f;
    /// <summary>
    /// Vida actual del orco invocador
    /// </summary>
    float vida;
    /// <summary>
    /// Colección de objetos en las posiciones a las que el orco invocador puede ir al teletransportarte
    /// </summary>
    public List<GameObject> teletransportes;
    /// <summary>
    /// Colección de objetos en las posiciones en las que aparecerán los enemigos al usar <see cref="invocar"/>
    /// </summary>
    public List<GameObject> posicionesInvocacion;
    /// <summary>
    /// Objeto que se invoca al usar <see cref="invocar"/>
    /// </summary>
    public GameObject invocacion;
    /// <summary>
    /// Tiempo que tiene que pasar después de que se llame a <see cref="invocar"/> para volver a llamarse
    /// </summary>
    public float cdInvocaciones=5f;
    /// <summary>
    /// Timer auxiliar para el cd de la invocación
    /// </summary>
    float timerInvocaciones;
    /// <summary>
    /// Tiempo que dura el color de dañado tras recibir un golpe
    /// </summary>
    public float tiempoCambioColor=1f;
    /// <summary>
    /// Estadísticas de la partida actual
    /// </summary>
    Estadisticas estadisticas;
    /// <summary>
    /// Booleana que indica si el orco invocador está vivo
    /// </summary>
    bool vivo =true;
    /// <summary>
    /// Objeto que se invoca cuando la vida del enemigo es igual o inferior a cero (Efecto de muerte)
    /// </summary>
    public GameObject muerte;
    /// <summary>
    /// Objeto que sea padre de todos los objetos de la sala del jefe
    /// </summary>
    public GameObject objetosSalaJefe;
    /// <summary>
    /// Timer auxiliar para el color del enemigo tras recibir un golpe
    /// </summary>
    float timerColorDañado;
    /// <summary>
    /// Animator que gestiona las animaciones del elemento que contiene esta clase.
    /// </summary>
    Animator animator;
    /// <summary>
    /// Color del orco invocador en estado normal
    /// </summary>
    Color normalColor;
    /// <summary>
    /// Bolean que indica si el enemigo está en enraged
    /// </summary>
    bool enraged = false;
    /// <summary>
    /// Bolean que especifica si al derrotar a este enemigo el juego termina
    /// </summary>
    public bool terminaJuego=true;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitada por primera vez antes de la función update.
    /// En ella se inicializan la variables en función a los elementos en la escena
    /// </summary>
    void Start()
    {
        timerInvocaciones = Time.time;
        estadisticas = GameObject.Find("Estadisticas").GetComponent<Estadisticas>();
        vida = vidaMax;
        normalColor= GetComponent<SpriteRenderer>().color;
        animator= GetComponent<Animator>();
    }
    /// <summary>
    /// Función que se encarga de gestionar la perdida de vida del orco invocador
    /// </summary>
    /// <param name="daño">Cantidad de vida que pierde el orco invocador</param>
    void damage(float daño)
    {
        vida = vida - daño;
    }
    /// <summary>
    /// Función que hace aparecer un objeto <see cref="invocacion"/> en uno de los puntos de la lista <see cref="posicionesInvocacion"/>
    /// </summary>
    void invocar()
    {
        if (posicionesInvocacion.Count > 0)
        {
            Instantiate(invocacion, posicionesInvocacion[Random.Range(0, posicionesInvocacion.Count)].transform.position, Quaternion.identity);
        }
    }
    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esté habilitado 
    /// </summary>
    void Update()
    {
        if(enraged==false && vida < vidaMax / 2)
        {
            cdInvocaciones = cdInvocaciones / 2;
            enraged = true;
        }
        if(Time.time - timerColorDañado > tiempoCambioColor && GetComponent<SpriteRenderer>().color != normalColor)
        {
            GetComponent<SpriteRenderer>().color = normalColor;
        }
        if (Time.time - timerInvocaciones > cdInvocaciones && vivo && !animator.GetBool("Invocando"))
        {
            //Debug.Log("Invoco");
            animator.SetBool("Invocando", true);
        }
        if (animator.GetBool("Invocando"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("OrcoInvocador_Cast") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime>= 1.0f)
            {
                //Debug.Log("TermineAnimacion");
                invocar();
                animator.SetBool("Invocando",false);
                timerInvocaciones = Time.time;
            }
        }
    }
    /// <summary>
    /// Calcula una posición de teletransporte
    /// </summary>
    /// <returns>Devuelve un objeto de la colección <see cref="teletransportes"/> </returns>
    GameObject ObtenerTeletransporte() {
            return teletransportes[Random.Range(0, teletransportes.Count)];
    }
    /// <summary>
    /// Función que se lanza cuando el objeto entra colisión
    /// </summary>
    /// <param name="collision">Objeto generado de por la colisión</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DañoJugador")
        {
            if (GetComponent<SpriteRenderer>().color !=Color.red) {
                GetComponent<SpriteRenderer>().color = Color.red;
                timerColorDañado = Time.time;
                damage(collision.gameObject.GetComponent<DamageJugador>().ataque);
                if (vida <= 0)
                {
                    if (estadisticas != null)
                    {
                        estadisticas.enemigosDerrotados++;
                    }
                    vivo = false;
                    Instantiate(muerte, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    if (!terminaJuego)
                    {
                        if (objetosSalaJefe == null)
                        {
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            Destroy(objetosSalaJefe);
                        }
                    }
                    else
                    {
                        GameObject.Find("UI").GetComponent<GestionPantallasPartida>().Fin=true;
                    }
                }
                else
                {
                    if (teletransportes.Count > 1)
                    {
                        //transform.position = teletransportes[Random.Range(0, teletransportes.Count)].transform.position;
                        Vector3 nuevaPos;
                        do
                        {
                            nuevaPos = ObtenerTeletransporte().transform.position;
                        } while (transform.position==nuevaPos);
                        //transform.position = teletransportes[Random.Range(0, teletransportes.Count)].transform.position;
                        transform.position = nuevaPos;
                    }
                }
            }
        }
    }
}
