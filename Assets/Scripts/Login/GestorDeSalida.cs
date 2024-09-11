using UnityEngine;

public class GestorDeSalida : MonoBehaviour
{  
    public void CerrarJuego()
    {    
        Emisor.Enviar((byte)Tipos.MensajeSaliente.MS_CIERRE_SESION, "");
        Application.Quit();
    }
}