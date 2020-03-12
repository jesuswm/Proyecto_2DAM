using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que se encarga de eliminar un objeto cuando termine la animación que este reproduciendo
/// </summary>
/// <remarks>
///  Para el funcionamiento de esta clase es necesario que el elemento también tenga asociado
/// <list type="bullet">
/// <item><see cref="Animator"/></item>
/// </list>
/// </remarks>
public class AnimarMuerteGenerica : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Animator que gestiona las animaciones del elemento que contiene esta clase.
    /// </summary>
    Animator animator;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitado por primera vez antes de update
    /// en ella se obtiene el <see cref="Animation"/> que posee el elemento
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esté habilitado
    /// en ella se hacen las comprobaciones de si la animación termino y eliminar el elemento si es necesario.
    /// </summary>
    void Update()
    {
        if (/*animator.GetCurrentAnimatorStateInfo(0).IsName("MuerteGenerica") &&*/
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
