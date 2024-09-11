using UnityEngine;

public class GestorMonstruos : MonoBehaviour
{
    private void Awake()
    {
        GestorEscena.Instancia.RegistrarGestor();
    }
}