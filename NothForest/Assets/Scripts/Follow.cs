using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que usa la cámara durante la partida para seguir al jugador y desactiva la música en función de las opciones
/// </summary>
/// <remarks>
/// Para el funcionamiento de esta clase es necesario que el elemento también  tenga asociado:
/// <list type="bullet">
/// <item><see cref="AudioSource"/></item>
/// <item><see cref="Transform"/></item>
/// </list>
/// </remarks>
public class Follow : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Posición actual de la cámara
    /// </summary>
    Transform pos;
    /// <summary>
    /// Rigidbody2D del objeto al que sigue la cámara
    /// </summary>
    public Rigidbody2D player;
    /// <summary>
    /// Función que se llama en cuanto el elemento que posee esta clase esté habilitade por primera vez antes de update.
    /// En ella se inicializan las variables y se mutea el AudioSource si el sonido esta desactivado.
    /// </summary>
    void Start()
    {
        if (player == null)
        {
            player=GameObject.Find("Jugador").GetComponent<Rigidbody2D>();
        }
        RegistroConfiguracion conf = GuardarCargarConf.cargarConfiguracion();
        if(conf != null)
        {
            if (conf.sonido)
            {
                GetComponent<AudioSource>().mute = false;
            }
            else
            {
                GetComponent<AudioSource>().mute = true;
            }
        }
        pos =GetComponent<Transform>();
    }

    /// <summary>
    /// Función que se llama cada frame mientras que el elemento que posee esta clase esta habilitado
    /// en ella se iguala la posición actual con la del objeto del parametro <see cref="player"/> 
    /// </summary>
    void Update()
    {
        pos.position = new Vector3(player.position.x, player.position.y, pos.position.z);
    }
}
