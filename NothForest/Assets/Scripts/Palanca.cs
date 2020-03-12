using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que gestiona el funcionamiento de la palanca haciendo aparecer o desaparecer el objeto del parámetro <see cref="objetoAsociado"/> de la escena
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que el elemento también  tenga asociado
/// <list type="bullet">
/// <item><see cref="Rigidbody2D"/></item>
/// <item><see cref="BoxCollider2D"/></item>
/// <item><see cref="Animator"/></item>
/// </list>
/// </remarks>
public class Palanca : MonoBehaviour
{
    /// <summary>
    /// Objeto que se hace aparecer o desaparecer de la escena
    /// </summary>
    public GameObject objetoAsociado;
    /// <summary>
    /// Objeto que aparece justo encima del objeto que se hace aparecer o desaparecer (Esta pensado para poner un objeto que reproduzca una animación)
    /// </summary>
    public GameObject EfectoActivar;
    /// <summary>
    /// Se puede volver a usar la palanca tras el primer uso
    /// </summary>
    public bool reversibe = true;
    /// <summary>
    /// Animator que gestiona las animaciones del elemento que contiene esta clase.
    /// </summary>
    Animator animator;
    /// <summary>
    /// Booleana que indica el estado de la palanca
    /// </summary>
    bool activo = false;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitado por primera vez antes de update.
    /// En ella se inicializan la variables en función a los elementos en la escena y se ajusta el tamaño del boxColaider al tamaño del menú
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("On", activo);
    }
    /// <summary>
    /// Función que se lanza cuando el objeto entra colisión
    /// </summary>
    /// <param name="collision">Objeto generado de por la colisión</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DañoJugador")
        {
            activo = !activo;
            animator.SetBool("On", activo);
            if (EfectoActivar != null) {
                Instantiate(EfectoActivar,objetoAsociado.transform.position,Quaternion.identity);
            }
            objetoAsociado.GetComponent<SpriteRenderer>().enabled = !objetoAsociado.GetComponent<SpriteRenderer>().enabled;
            objetoAsociado.GetComponent<Collider2D>().enabled = !objetoAsociado.GetComponent<Collider2D>().enabled;
            if (!reversibe)
            {
                Destroy(GetComponent<Collider2D>());
            }
        }
    }
}
