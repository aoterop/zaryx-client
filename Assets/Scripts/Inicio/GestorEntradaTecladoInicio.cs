using UnityEngine;
using UnityEngine.InputSystem;

public class GestorEntradaTecladoInicio : MonoBehaviour
{
    public GestorIntroVideo gestorVideo;

    private InputAction skipVideo;   

    void OnEnable()
    {
        // Define las acciones a realizar para cada tecla.
        skipVideo = new InputAction("SkipVideo", InputActionType.Button, "<Keyboard>/enter");
        skipVideo.performed += context => gestorVideo.SkipIntroVideo();

        // Habilita las acciones para que puedan ser activadas.
        skipVideo.Enable();   
    }

    void OnDisable()
    {// Deshabilita las acciones cuando el script es deshabilitado.
        skipVideo.Disable();     
    }
}
