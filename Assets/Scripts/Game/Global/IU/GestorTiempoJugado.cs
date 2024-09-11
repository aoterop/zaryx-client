using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestorTiempoJugado : MonoBehaviour
{
    public TextMeshProUGUI Tiempo;

    private bool Activo = false;
    private int tiempoJugado;
    private Coroutine corrutina;

    public void VisibilidadTiempoJugado()
    {
        if(!Activo)
        {
            Abrir();
        }
        else
        {
            Cerrar();
        }
    }

    public void Cerrar()
    {
        CambiarVisibilidadHijos(gameObject, 0f, false);
        Activo = false;
    }

    public void Abrir()
    {
        CambiarVisibilidadHijos(gameObject, 1f, true);
        Activo = true;
    }

    public void CambiarVisibilidadHijos(GameObject parentObject, float alpha, bool raycast)
    {
        Image[] imagenes = parentObject.GetComponentsInChildren<Image>(true);
        foreach (Image imagen in imagenes)
        {
            UnityEngine.Color color = imagen.color;
            color.a = alpha;
            imagen.color = color;
            imagen.raycastTarget = raycast;
        }

        RawImage[] imagenesRaw = parentObject.GetComponentsInChildren<RawImage>(true);
        foreach (RawImage imagenRaw in imagenesRaw)
        {
            UnityEngine.Color color = imagenRaw.color;
            color.a = alpha;
            imagenRaw.color = color;
            imagenRaw.raycastTarget = raycast;
        }

        TextMeshProUGUI[] textos = parentObject.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI texto in textos)
        {
            UnityEngine.Color color = texto.color;
            color.a = alpha;
            texto.color = color;
            texto.raycastTarget = raycast;
        }
    }

    public void InicializarTemporizador(int tiempoAcumulado)
    {
        tiempoJugado = tiempoAcumulado;
        corrutina = StartCoroutine(Tempo());
        Cerrar();
    }

    public IEnumerator Tempo()
    {
        while (true)
        {
            tiempoJugado++;

            int dias = tiempoJugado / 86400;
            int horas = (tiempoJugado % 86400) / 3600;
            int minutos = (tiempoJugado % 3600) / 60;
            int segundos = tiempoJugado % 60;

            Tiempo.text = string.Format("{0}d {1}h {2}m {3}s", dias, horas, minutos, segundos);

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDestroy()
    {
        if (corrutina != null)
        {
            StopCoroutine(corrutina);
        }
    }
}