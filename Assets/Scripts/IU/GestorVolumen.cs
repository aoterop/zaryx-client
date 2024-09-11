using UnityEngine;
using UnityEngine.UI;

public class GestorVolumen : MonoBehaviour
{
    [SerializeField]
    public Slider barraDeVolumen;

    private float sonido;

    private void Awake()
    {
        sonido = PlayerPrefs.GetFloat("Sonido", 0.5f);
        LoadData();
    }

    public void AjustarVolumen()
    {
        sonido = barraDeVolumen.value;
        PlayerPrefs.SetFloat("Sonido", sonido);
        AudioListener.volume = sonido;
    }

    private void CorregirSonido()
    {
        if (sonido < 0 || sonido > 1)
        {
            sonido = 0;
            PlayerPrefs.SetFloat("Sonido", 0);
            barraDeVolumen.value = 0;
            AudioListener.volume = sonido;
        }
    }

    public void LoadData()
    {
        CorregirSonido();
        barraDeVolumen.value = PlayerPrefs.GetFloat("Sonido", sonido);
        AudioListener.volume = sonido;
    }
}