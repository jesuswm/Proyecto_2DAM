using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase serializable que se usa para almacenar los datos de estadísticas
/// </summary>
[System.Serializable]
public class RegistroEstadisticas 
{
    /// <summary>
    /// Número de enemigos derrotados
    /// </summary>
    int enemigosDerrotados;
    /// <summary>
    /// Devuelve el valor de la variable enemigosDerrotados
    /// </summary>
    public int EnemigosDerrotados
    {
        get
        {
            return enemigosDerrotados;
        } 
    }
    /// <summary>
    /// Número de ataques realizados
    /// </summary>
    int ataquesRealizados;
    /// <summary>
    /// Devuelve el valor de la variable ataquesRealizados
    /// </summary>
    public int AtaquesRealizados
    {
        get
        {
            return ataquesRealizados;
        }
    }
    /// <summary>
    /// Tiempo de duración de la partida
    /// </summary>
    float maximaDuracionDePartida;
    /// <summary>
    /// Devuelve el valor de la variable maximaDuracionDePartida
    /// </summary>
    public float MaximaDuracionDePartida
    {
        get
        {
            return maximaDuracionDePartida;
        }
    }
    /// <summary>
    /// Constructo que inicializa todas las variables con valor 0
    /// </summary>
    public RegistroEstadisticas()
    {
        this.enemigosDerrotados = 0;
        this.ataquesRealizados = 0;
        this.maximaDuracionDePartida = 0;
    }
    /// <summary>
    /// Constructo que inicializa las variables en función de los parámetros
    /// </summary>
    /// <param name="enemigosDerrotados">Valor que se asignara a la variable enemigosDerrotados</param>
    /// <param name="ataquesRealizados">Valor que se asignara a la variable ataquesRealizados</param>
    /// <param name="maximaDuracionDePartida">Valor que se asignara a la variable maximaDuracionDePartida</param>
    public RegistroEstadisticas(int enemigosDerrotados,int ataquesRealizados,float maximaDuracionDePartida)
    {
        this.enemigosDerrotados = enemigosDerrotados;
        this.ataquesRealizados = ataquesRealizados;
        this.maximaDuracionDePartida = maximaDuracionDePartida;
    }
    /// <summary>
    /// Constructor que permite la creación de un nuevo <see cref="RegistroEstadisticas"/> en base a un objeto de la clase <see cref="Estadisticas"/>
    /// </summary>
    /// <param name="estadisticas">Parámetro en base al cual se inicializan las variables del objeto</param>
    public RegistroEstadisticas(Estadisticas estadisticas)
    {
        this.enemigosDerrotados = estadisticas.enemigosDerrotados;
        this.ataquesRealizados = estadisticas.ataquesRealizados;
        this.maximaDuracionDePartida = estadisticas.inicioDePartida;
    }
    /// <summary>
    /// Función el valor de la variable tiempo en formato mm:ss
    /// </summary>
    public string tiempoFormateado()
    {
        TimeSpan ts = TimeSpan.FromSeconds(maximaDuracionDePartida);
        return string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }
}
