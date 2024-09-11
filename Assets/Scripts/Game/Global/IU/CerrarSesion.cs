using UnityEngine;

public class CerrarSesion : MonoBehaviour
{
    public void CerrarSession()
    {
        GestorDeEventos.CambiarEscena.Invoke("Login");
    }
}