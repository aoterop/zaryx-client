using UnityEngine;

public class PersonajeSeleccionado : MonoBehaviour
{
    public static PersonajeSeleccionado Instancia { get; private set; }
    public IPersonaje Personaje { get; set; }
    public byte Clase { get; set; }

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
}