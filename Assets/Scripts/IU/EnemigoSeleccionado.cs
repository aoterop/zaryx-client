using UnityEngine;
using UnityEngine.UI;

public class EnemigoSeleccionado : MonoBehaviour
{
    [SerializeField]
    public Image barraHpEnemigo;
    public Image barraMpEnemigo;

    private void Start()
    {
    }

    public void OcultarPanel()
    {
        gameObject.SetActive(false);
    }
}
