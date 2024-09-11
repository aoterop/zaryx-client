using UnityEngine;

public class GestorEscena : MonoBehaviour
{
    public static GestorEscena Instancia { get; private set; }

    private const byte _gestoresTotales = 6;
    private byte _gestoresRegistrados = 0;
    private object locker = new object();

    private void Awake()
    {
        if(Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestablecerGestores()
    {
        lock(locker)
        {
            _gestoresRegistrados = 0;
        }
    }

    public void RegistrarGestor()
    {
        lock(locker)
        {
            _gestoresRegistrados += 1;

            if(_gestoresRegistrados >= _gestoresTotales)
            {
                GestorPersonaje.Instancia.NotificarCambioMapa();
                _gestoresRegistrados = 1; // El gestor del personaje solo se registra una vez.
            }
        }
    }
}