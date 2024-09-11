using UnityEngine;

public class GestorCursorInicio : MonoBehaviour
{
    void Awake()
    {// Ocultar el cursor.
        Cursor.visible = false;
        QualitySettings.vSyncCount = 1;
    }
}
