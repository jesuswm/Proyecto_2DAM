using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que se encarga de bloquear y hacer aparecer al jefe en la escena del juego.
/// </summary>
public class BloquearSalaJeje : MonoBehaviour
{
    /// <summary>
    /// Objeto que contenga la clase <see cref="OrcoInvocador"/> en la escena
    /// </summary>
    GameObject jefe;
    /// <summary>
    /// Archivo de audio que se reproducirá al activar el jefe
    /// </summary>
    public AudioClip musicaJefe;
    /// <summary>
    /// Función que se llama antes del start en ella se inicializan las variables
    /// </summary>
    void Awake()
    {
        jefe = GameObject.Find("Jefe");
        jefe.SetActive(false);
    }
    /// <summary>
    /// Función que se lanza cuando el objeto entra en colisión
    /// </summary>
    /// <param name="collision">Objeto generado de por la colisión</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Find("Main Camera").gameObject.GetComponent<AudioSource>().clip=musicaJefe;
        GameObject.Find("Main Camera").gameObject.GetComponent<AudioSource>().Play();
        jefe.SetActive(true);
        Destroy(this.gameObject);
    }
 }
