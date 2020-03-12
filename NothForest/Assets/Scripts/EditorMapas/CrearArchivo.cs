using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class CrearArchivo
{
    public static void GuardarObjetosMapa()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string patch = Application.persistentDataPath + "/mapa.dat";
        FileStream stream = new FileStream(patch, FileMode.Create);
        List<ObjetoMapa> objetos = new List<ObjetoMapa>();
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 0, 0, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 1, 0, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 2, 0, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 3, 0, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 4, 0, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 5, 0, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 6, 0, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 7, 0, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 8, 0, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 0, 1, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 1, 1, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 2, 1, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 3, 1, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 4, 1, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 5, 1, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 6, 1, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 7, 1, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Cesped, eTipo.TerrenoTras, 8, 1, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 0, 2, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 1, 2, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 2, 2, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 3, 2, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 4, 2, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 5, 2, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 6, 2, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 7, 2, 0));
        objetos.Add(new ObjetoMapa((int)eTiles.Agua, eTipo.TerrenoNoTras, 8, 2, 0));
        objetos.Add(new ObjetoMapa((int)eEnemigo.Tronquito, eTipo.Enemigo, 4, 1, 0));
        formatter.Serialize(stream, objetos);
        stream.Close();
        
    }
    public static List<ObjetoMapa> cargarObjetosMapa()
    {
        string patch = Application.persistentDataPath + "/mapa.dat";
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
}
