using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Supervisor : MonoBehaviour
{
    public static Supervisor Instancia { get ; private set; }
    public GameObject panelError;
    public TextMeshProUGUI mensajeError;

    private void Awake()
    {
        if(Instancia == null)
        {
            Instancia = this;
        }

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("PanelError"))
        {
            if(obj.name == "Panel Mensaje de Error")
            {
                panelError = obj;
            }
        }
        mensajeError = panelError.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Supervisar(bool estadoDeRed)
    {
        if (!estadoDeRed)
        {// Si la conexión se ha perdido...

            string escenaActual = SceneManager.GetActiveScene().name;

            if (escenaActual != "Login")
            {// LLevamos al usuario a la escena de login.

                try { GestorDeEventos.CambiarEscena.Invoke("Login"); } catch { }
            }
        }
    }
}