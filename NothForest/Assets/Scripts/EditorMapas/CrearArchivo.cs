using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
/// <summary>
/// Clase que permite guardar y cargar un mapa
/// </summary>
public static class CrearArchivo
{
    /// <summary>
    /// Función que lee el archivo .map pasado como parámetro y devuelve los datos del mapa guardado
    /// </summary>
    /// <param name="nombre">Nombre del archivo .map que queremos cargar</param>
    /// <returns>Devuelve una lista de <see cref="ObjetoMapa"/> que contiene todos los objetos que tiene el mapa</returns>

    public static List<ObjetoMapa> cargarObjetosMapa(string nombre)
    {
        string patch = Application.persistentDataPath + "/"+ nombre+".map";
        if (File.Exists(patch))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(patch, FileMode.Open);
            List<ObjetoMapa> objetos = formatter.Deserialize(stream) as List<ObjetoMapa>;
            stream.Close();
            return objetos;
        }
        else
        {
            Debug.Log("No existe el archivo");
            return null;
        }
    }
    /// <summary>
    /// Función que elimina el archivo de de un mapa .map 
    /// </summary>
    /// <param name="nombre">nombre del mapa que queremos borrar</param>
    public static void BorrarArchivoMapa(string nombre)
    {
        string patch = Application.persistentDataPath + "/" + nombre + ".map";
        if (File.Exists(patch))
        {
            File.Delete(patch);
        }
        else
        {
            Debug.Log("No existe el archivo confi.dat");
        }
    }
}
