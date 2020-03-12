using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/// <summary>
/// Clase estática con las funciones de carga y guardado de la configuración y las estadísticas.
/// </summary>
public static class GuardarCargarConf 
{
    /// <summary>
    /// Función que guarda la configuración en congi.dat
    /// </summary>
    /// <param name="configuracion">Configuración que queremos guardar</param>
    public static void GuardarConfiguracion(Configuracion configuracion)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string patch = Application.persistentDataPath + "/confi.dat";
        FileStream stream = new FileStream(patch,FileMode.Create);
        RegistroConfiguracion registro = new RegistroConfiguracion(configuracion);
        formatter.Serialize(stream,registro);
        stream.Close();
    }
    /// <summary>
    /// Función que guarda las estadísticas en estadisticas.dat si alguno de los valores es mejor que los actuales.
    /// </summary>
    /// <param name="estadisticas">estadísticas que queremos guardar</param>
    public static void GuardarEstadisticas(Estadisticas estadisticas)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string patch = Application.persistentDataPath + "/estadisticas.dat";
        if (!File.Exists(patch))
        {
            FileStream stream = new FileStream(patch, FileMode.Create);
            RegistroEstadisticas registro = new RegistroEstadisticas(estadisticas.enemigosDerrotados,estadisticas.ataquesRealizados, Time.time - estadisticas.inicioDePartida);
            formatter.Serialize(stream, registro);
            stream.Close();
        }
        else
        {
            RegistroEstadisticas estadisticasActuales = cargarEstadiscas();
            int enemigosDerrotados;
            if (estadisticasActuales.EnemigosDerrotados >= estadisticas.enemigosDerrotados)
            {
                enemigosDerrotados = estadisticasActuales.EnemigosDerrotados;
            }
            else
            {
                enemigosDerrotados = estadisticas.enemigosDerrotados;
            }
            int ataquesRealizados;
            if (estadisticasActuales.AtaquesRealizados >= estadisticas.ataquesRealizados)
            {
                ataquesRealizados = estadisticasActuales.AtaquesRealizados;
            }
            else
            {
                ataquesRealizados = estadisticas.ataquesRealizados;
            }
            float maximaDuracionDePartida;
            float tiempo = Time.time;
            if (estadisticasActuales.MaximaDuracionDePartida >= tiempo-estadisticas.inicioDePartida)
            {
                maximaDuracionDePartida = estadisticasActuales.MaximaDuracionDePartida;
            }
            else
            {
                maximaDuracionDePartida = tiempo - estadisticas.inicioDePartida;
            }
            FileStream stream = new FileStream(patch, FileMode.Create);
            RegistroEstadisticas registro = new RegistroEstadisticas(enemigosDerrotados,ataquesRealizados,maximaDuracionDePartida);
            formatter.Serialize(stream, registro);
            stream.Close();
        }
    }
    /// <summary>
    /// Función que carga la configuración guardada en confi.dat
    /// </summary>
    /// <returns>Si existe nos devolvera un objeto de tipo RegistroConfiguracion con la configuración actual si no existe devuelve null</returns>
    public static RegistroConfiguracion cargarConfiguracion()
    {
        string patch = Application.persistentDataPath + "/confi.dat";
        if (File.Exists(patch))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(patch,FileMode.Open);
            RegistroConfiguracion registro = formatter.Deserialize(stream) as RegistroConfiguracion;
            stream.Close();
            return registro;
        }
        else
        {
            Debug.Log("No existe el archivo");
            return null;
        }

    }
    /// <summary>
    /// Función que carga las estadísticas guardadas en estadisticas.dat
    /// </summary>
    /// <returns>Si existe nos devolvera un objeto de tipo RegistroEstadisticas con las estadísticas actuales si no existe devuelve null</returns>
    public static RegistroEstadisticas cargarEstadiscas()
    {
        string patch = Application.persistentDataPath + "/estadisticas.dat";
        if (File.Exists(patch))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(patch, FileMode.Open);
            RegistroEstadisticas registro = formatter.Deserialize(stream) as RegistroEstadisticas;
            stream.Close();
            return registro;
        }
        else
        {
            Debug.Log("No existe el archivo");
            return null;
        }

    }
    /// <summary>
    /// Función que elimina el archivo de confi.dat donde se guarda la configuración
    /// </summary>
    public static void BorrarConfiguracion()
    {
        string patch = Application.persistentDataPath + "/confi.dat";
        if (File.Exists(patch))
        {
            File.Delete(patch);
        }
        else
        {
            Debug.Log("No existe el archivo confi.dat");
        }
    }
    /// <summary>
    /// Función que elimina el archivo de confi.dat donde se guarda la estadísticas
    /// </summary>
    public static void BorrarEstadisticas()
    {
        string patch = Application.persistentDataPath + "/estadisticas.dat";
        if (File.Exists(patch))
        {
            File.Delete(patch);
        }
        else
        {
            Debug.Log("No existe el archivo estadisticas.dat");
        }
    }
}
