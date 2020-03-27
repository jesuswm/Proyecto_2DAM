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
        Jugador jugador= GameObject.Find("Jugador").GetComponent<Jugador>(); 
        atacar.onClick.AddListener(delegate () { jugador.ataque(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
