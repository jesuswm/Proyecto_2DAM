using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que gestiona el desplazamiento de la cámara en el editor
/// </summary>
public class MoverCamara : MonoBehaviour
{
    public GameObject camera_GameObject;

    Vector2 puntoInicio;
    Vector2 inicioArrastre;
    Vector2 finArrastre;
    Vector2 posicionDedo0;
    float DistanciaEntreDedos;
    bool zoom=false;
    public bool activo = true;
    GameObject PanelHerramientas;
    float maxZom = 3.5f;
    float minZom = 0.5f;
    Vector3 posicionInicio;
    Vector3 Origen;
    float Velocidad=1f;
    void Start()
    {
        PanelHerramientas = GameObject.Find("Panel");
    }
    void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            if (!PanelHerramientas.activeSelf || GameObject.Find("TogleHerramientas").GetComponent<SeleccionDeHerramienta>().herramientaActual == eHerramientas.mover)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    posicionInicio = transform.position;
                    Origen = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                }

                if (Input.GetMouseButton(0))
                {
                    Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - Origen;
                    camera_GameObject.transform.position = posicionInicio + -pos * Velocidad;
                }
                if (Input.GetAxis("Mouse ScrollWheel") > 0f && camera_GameObject.GetComponent<Camera>().orthographicSize > minZom) 
                {
                    camera_GameObject.GetComponent<Camera>().orthographicSize= camera_GameObject.GetComponent<Camera>().orthographicSize-0.1f;
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f && camera_GameObject.GetComponent<Camera>().orthographicSize < maxZom) 
                {
                    camera_GameObject.GetComponent<Camera>().orthographicSize = camera_GameObject.GetComponent<Camera>().orthographicSize + 0.1f;
                }
            }
        }
        else
        {
            if (!PanelHerramientas.activeSelf || GameObject.Find("TogleHerramientas").GetComponent<SeleccionDeHerramienta>().herramientaActual == eHerramientas.mover)
            {
                if (Input.touchCount == 0 && zoom)
                {
                    zoom = false;
                }

                if (Input.touchCount == 1)
                {
                    if (!zoom)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Moved)
                        {
                            Vector2 nuevaPosicion = GetWorldPosition();
                            Vector2 PositionDifference = nuevaPosicion - puntoInicio;
                            camera_GameObject.transform.Translate(-PositionDifference);
                        }
                        puntoInicio = GetWorldPosition();
                    }
                }
                else if (Input.touchCount == 2)
                {
                    if (Input.GetTouch(1).phase == TouchPhase.Moved)
                    {
                        zoom = true;

                        finArrastre = GetWorldPositionDedo(1);
                        Vector2 diferenciaEntrePosiciones = finArrastre - inicioArrastre;

                        if (Vector2.Distance(finArrastre, posicionDedo0) < DistanciaEntreDedos)
                        {
                            if (camera_GameObject.GetComponent<Camera>().orthographicSize + diferenciaEntrePosiciones.magnitude <= maxZom)
                            {
                                camera_GameObject.GetComponent<Camera>().orthographicSize += (diferenciaEntrePosiciones.magnitude);
                            }
                        }

                        if (Vector2.Distance(finArrastre, posicionDedo0) >= DistanciaEntreDedos)
                        {
                            camera_GameObject.GetComponent<Camera>().orthographicSize -= (diferenciaEntrePosiciones.magnitude);
                        }

                        DistanciaEntreDedos = Vector2.Distance(finArrastre, posicionDedo0);
                    }
                    inicioArrastre = GetWorldPositionDedo(1);
                    posicionDedo0 = GetWorldPositionDedo(0);
                }
            }
            else
            {
                zoom = false;
            }
        }
    }

    Vector2 GetWorldPosition()
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2 GetWorldPositionDedo(int dedo)
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(dedo).position);
    }
}
