using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GestorIntroVideo : MonoBehaviour
{
    [SerializeField] public VideoPlayer videoPlayer;

    private object o = new object();
    private bool cambiandoEscena = false;

    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Sonido", 0.5f);
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer videoPlayer)
    {// El vídeo ha finalizado.
        SkipIntroVideo();
    }


    public void SkipIntroVideo()
    {// Carga de la escena de Login con exclusión mutua.

        lock (o)
        {
            if (!cambiandoEscena)
            {
                cambiandoEscena = true;
                SceneManager.LoadScene("Login");
            }
        }
    }
}