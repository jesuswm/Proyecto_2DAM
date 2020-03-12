using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que tras reproducir la animación actual invoca uno de los objetos de la lista <see cref="invocaciones"/> y después se destruye.
/// </summary>
/// <remarks>
///  Para el funcionamiento de esta clase es necesario que el elemento también tenga asociado
/// <list type="bullet">
/// <item><see cref="Animator"/></item>
/// </list>
/// </remarks>
public class InvocarEnemigo : MonoBehaviour
{
    /// <summary>
    /// Lista de objetos que pueden aparecer cuando se termine la animacion actual del objeto 
    /// </summary>
    public List<GameObject> invocaciones;
    /// <summary>
    /// Animator que gestiona las animaciones del elemento que contiene esta clase.
    /// </summary>
    Animator animator;
    /// <summary>
    /// Función que se llama en cuanto el elemento que pose esta clase esté habilitado por primera vez antes de update
    /// en ella se obtiene el <see cref="Animation"/> que posee el elemento
    /// </summary>
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase está habilitado
    /// en ella se hacen las comprobaciones de si la animación termino en caso afirmativo invoca un objeto de la lista y elimina el objeto.
    /// </summary>
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (GameObject.Find("Jefe") != null)
            {
                Instantiate(invocaciones[Random.Range(0, invocaciones.Count)], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity).transform.parent = GameObject.Find("Jefe").transform;
            }
            else
            {
                Instantiate(invocaciones[Random.Range(0, invocaciones.Count)], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

}
