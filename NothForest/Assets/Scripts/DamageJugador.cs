using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que gestiona objetos que causan daño a los enemigos(ejemplo la espada del jugador)
/// </summary>
public class DamageJugador : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Daño que hace el objeto a los enemigos
    /// </summary>
    public float ataque=1f;
    /// <summary>
    /// Tiempo que dura el objeto en la escena
    /// </summary>
    public float duracion = 0.3f;
    /// <summary>
    /// El objeto que hizo aparecer a este objeto
    /// </summary>
    public GameObject invocador;
    /// <summary>
    /// Timer para controlar la duración del objeto
    /// </summary>
    float timer;
    /// <summary>
    /// Función que permite estableces todos los parámetros del objeto
    /// </summary>
    /// <param name="ataque">Establece el valor de ataque del objeto</param>
    /// <param name="duracion">Establece el valor de la duración objeto</param>
    /// <param name="invocador">Establece el quie es el invocador del objeto</param>
    public void establecerPropiedades(float ataque, float duracion,GameObject invocador)
    {
        this.ataque = ataque;
        this.duracion = duracion;
        this.invocador = invocador;
        transform.parent = invocador.transform;
    }
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esta habilitado por primera vez antes de update.
    /// </summary>
    void Start()
    {
        timer = Time.time;
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esté habilitado. 
    /// </summary>
    void Update()
    {
        if (Time.time - timer > duracion)
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Función que se lanza cuando el objeto entra en colisión
    /// </summary>
    /// <param name="col">Objeto generado por la colisión</param>
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("OnCollisionEnter2D");
        if (col.gameObject.tag == "Enemigo")
        {
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}
