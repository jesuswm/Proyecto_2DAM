using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonAtacarAutoGenerado : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button atacar = gameObject.GetComponent<Button>();
        try
        {
            Jugador jugador = GameObject.Find("Jugador").GetComponent<Jugador>();
            atacar.onClick.AddListener(delegate () { jugador.ataque(); });
        }
        catch (NullReferenceException)
        {
            Debug.Log("Script BotonAtacarAutoGenerado: No se encontro el componente \"Jugador\" deL GameObject Jugador");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
