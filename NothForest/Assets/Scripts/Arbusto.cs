using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que gestiona todas las variables y funciones del arbusto.
/// </summary>
/// <remarks>
///  Para el funcionamiento de esta clase es necesario que el elemento también  tenga asociado
/// <list type="bullet">
/// <item><see cref="Rigidbody2D"/></item>
/// <item><see cref="Animator"/></item>
/// <item><see cref="SpriteRenderer"/></item>
/// </list>
/// </remarks>
public class Arbusto : MonoBehaviour
{
    /// <summary>
    /// Animator que gestiona las animaciones del elemento que contiene esta clase.
    /// </summary>
    Animator animator;
    /// <summary>
    /// Lista de objetos que pueden aparecer cuando se destruye el arbusto 
    /// </summary>
    public List<GameObject> botin;
    /// <summary>
    /// Probabilidad de que aparezcan objetos de la lista <see cref="botin"/> al destruir el arbusto 0 es que nunca aparecen y 1 siempre aparece un objeto.
    /// </summary>
    public float probBotin=0.3f;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitado por primera vez antes de update
    /// En ella se inicializan la variables
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Función que comprueba si la animacion actual es "Arbusto_Cortar" en caso de que lo sea y terminara la animación
    /// se elimina el elemento con esta clase.
    /// </summary>
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Arbusto_Cortar") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Función que se lanza cuando el objeto entra colisión
    /// </summary>
    /// <param name="collision">Objeto generado de por la colisión</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Colision");
        if (collision.gameObject.tag == "DañoJugador")
        {
            //Debug.Log("Colision con arma");
            if (botin.Count > 0)
                {
                    if (Random.Range(0f, 1f) <= probBotin)
                    {
                        Instantiate(botin[Random.Range(0, botin.Count)], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    }
                }
            animator.SetBool("Cortar", true);
            Destroy(GetComponent<CircleCollider2D>());
        }
    }
}
