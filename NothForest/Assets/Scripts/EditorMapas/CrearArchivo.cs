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
    /// Función que genera un mapa predefinido de ejemplo
    /// </summary>
    [Obsolete("Este metodo se uso para realizar pruebas actualmente ya no se usa")]
    public static void GuardarObjetosMapa()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string patch = Application.persistentDataPath + "/mapa.dat";
        FileStream stream = new FileStream(patch, FileMode.Create);
        List<ObjetoMapa> objetos = new List<ObjetoMapa>();
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 0, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 1, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 2, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 3, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 4, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 5, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 6, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 7, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 8, 0, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 0, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 1, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 2, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 3, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 4, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 5, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 6, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 7, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 8, 1, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 0, 2, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 1, 2, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 2, 2, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 3, 2, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 4, 2, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 5, 2, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 6, 2, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 7, 2, 0));
        //objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 8, 2, 0));
        //objetos.Add(new ObjetoMapa((int)eEnemigo.Tronquito, eTipo.Enemigo, 4, 1, 0));
        formatter.Serialize(stream, objetos);
        stream.Close();
        
    }
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
