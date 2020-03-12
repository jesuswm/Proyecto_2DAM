using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que gestiona objetos que recuperan vida del jugador (corazones)
/// </summary>
public class ObjetoCura : MonoBehaviour
{
    /// <summary>
    /// Cantidad de vida que restaura al jugador
    /// </summary>
    public float curacion = 1;
    /// <summary>
    /// Tiempo que dura en la escena el objeto
    /// </summary>
    public float duracion = 0.3f;
    /// <summary>
    /// Timer para la duración del objeto
    /// </summary>
    float timer;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase está habilitado por primera vez antes de update
    /// </summary>
    void Start()
    {
        timer = Time.time;
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase está habilitado
    /// en ella se hacen las comprobaciones del tiempo que lleva en escena el objeto si este supera al asignado en <see cref="duracion"/> el objeto se destruye.
    /// </summary>
    void Update()
    {
        float tiempo = Time.time - timer;
        if (tiempo > duracion)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Color color = GetComponent<SpriteRenderer>().color;
            if (tiempo > duracion / 3) {
                if(tiempo > duracion / 2) {
                    if(tiempo > duracion / 1.2)
                    {
                        color.a = 0.25f;
                        GetComponent<SpriteRenderer>().color = color;
                    }
                    else
                    {
                        color.a = 0.5f;
                        GetComponent<SpriteRenderer>().color = color;
                    }
                }
                else
                {
                    color.a = 0.75f;
                    GetComponent<SpriteRenderer>().color = color;
                }
            }
        }
    }
    /// <summary>
    /// Función que se lanza cuando el objeto entra colisión
    /// </summary>
    /// <param name="col">Objeto generado por la colisión</param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Jugador")
        {
            Destroy(this.gameObject);
        }
    }
}
