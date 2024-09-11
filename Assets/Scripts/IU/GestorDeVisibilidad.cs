using UnityEngine;

public class GestorDeVisibilidad : MonoBehaviour
{    
    public void Ocultar()
    {
        gameObject.SetActive(false);
    }

    public void Mostrar()
    {
        if(!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }
}
