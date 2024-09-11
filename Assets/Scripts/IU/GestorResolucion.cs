using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GestorResolucion : MonoBehaviour
{
    public TMP_Dropdown resolucionesDropDown;
    public Toggle toggle;
    Resolution[] resoluciones;
    List<Resolution> resos = new List<Resolution>();

    private int screenWidth;
    private int screenHeight;
    private int isFullScreen;

    private void Awake()
    {
        screenHeight = PlayerPrefs.GetInt("ScreenHeight", 720);
        screenWidth = PlayerPrefs.GetInt("ScreenWidth", 1280);
        isFullScreen = PlayerPrefs.GetInt("IsFullScreen", 0);

        toggle.isOn = (isFullScreen == 1) ? true : false;        
        Screen.fullScreen = toggle.isOn;
    }

    void Start()
    {
        Resolution currentResolution = Screen.currentResolution;
        int screenWidth = currentResolution.width;
        int screenHeight = currentResolution.height;

        if (screenWidth >= 3840 && screenHeight >= 2160)
        {
            //Debug.Log("La resolución actual de la pantalla es 2K");
        }
        else if (screenWidth >= 1920 && screenHeight >= 1080)
        {
            //Debug.Log("La resolución actual de la pantalla es Full HD");
        }
        else
        {
            //Debug.Log("La resolución actual de la pantalla es HD");
        }

        RevisarResolucion();
    }

    public void CambiarPantallaCompleta()
    {
        Screen.fullScreen = toggle.isOn;
        isFullScreen = (toggle.isOn) ? 1 : 0;
        PlayerPrefs.SetInt("IsFullScreen", isFullScreen);
    }    

    private void RevisarResolucion()
    {
        Resolution currentResolution = Screen.currentResolution;
        resoluciones = Screen.resolutions;
        resolucionesDropDown.ClearOptions();

        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        int counter = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            int ancho = resoluciones[i].width;
            int alto = resoluciones[i].height;

            if
            ((ancho == 1280 && alto == 720) || /* HD */
                (ancho == 1920 && alto == 1080) || /* Full HD */
                (ancho == 2560 && alto == 1440) || /* 2K */
                (ancho == 3840 && alto == 2160)  /* 4K */)
            {
                string opcion = ancho.ToString() + " x " + alto.ToString();

                if (!opciones.Contains(opcion))
                {
                    if (Screen.width == ancho && Screen.height == alto)
                    {// Resolución del juego (no la de la pantalla : Screen.width != Screen.currentResolution.width).
                        resolucionActual = counter;
                    }

                    counter++;
                    opciones.Add(opcion);
                    resos.Add(resoluciones[i]);
                }
            }
        }

        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        Resolution resolucion = resos[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ScreenHeight", resolucion.height);
        PlayerPrefs.SetInt("ScreenWidth", resolucion.width);
    }
}