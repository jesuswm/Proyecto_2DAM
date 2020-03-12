using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase serializable que se usa para almacenar la configuración del juego
/// </summary>
[System.Serializable]
public class RegistroConfiguracion
{
    /// <summary>
    /// Booleana que indica si esta activa la vibracion del juego
    /// </summary>
    public bool vibracion;
    /// <summary>
    /// Booleana que indica si esta activo el sonido
    /// </summary>
    public bool sonido;
    /// <summary>
    /// Índice del idioma de la aplicación
    /// </summary>
    public int idioma;
    /// <summary>
    /// Constructor que permite la creación de un nuevo <see cref="RegistroConfiguracion"/> en base a un objeto de la clase <see cref="Configuracion"/>
    /// </summary>
    /// <param name="configuracion">Parámetro en base al cual se inicializan las variables del objeto</param>
    public RegistroConfiguracion(Configuracion configuracion)
    {
        vibracion = configuracion.vibracion;
        sonido = configuracion.sonido;
        idioma = configuracion.idioma;
    }
}
