using TMPro;
using UnityEngine;

public class GestorNombre : MonoBehaviour
{
    private Quaternion rotacion = Quaternion.Euler(32f, 0f, 0f);

    public TextMeshPro nickname;

    private void LateUpdate()
    {
        transform.rotation = rotacion;
    }

    public void EstablecerNombre(string nombre)
    {
        nickname.text = nombre;
    }
}