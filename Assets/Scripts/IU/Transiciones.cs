using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transiciones : MonoBehaviour
{
    public static Transiciones Instancia { get; private set; }

    public AnimationClip cambioEscena;


    private Animator animator;    
    private Canvas canvas;

    private void Awake()
    {
        if(Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);

            animator = GetComponentInChildren<Animator>();            
            canvas = GetComponent<Canvas>();

            canvas.enabled = false;

            GestorDeEventos.CambiarEscena += CambiarEscena;
        }
        else
        {          
            Destroy(gameObject);   
        }
    }


    public void CambiarEscena(string nuevaEscena)
    {
        StartCoroutine(Fundido(nuevaEscena));
    }

    private IEnumerator Fundido(string nuevaEscena)
    {
        canvas.enabled = true;

        animator.Play("CambioEscena");

        yield return new WaitForSeconds(cambioEscena.length / 2);

        SceneManager.LoadScene(nuevaEscena);

        yield return new WaitForSeconds(cambioEscena.length / 2);

        yield return new WaitForEndOfFrame();

        canvas.enabled = false;
    }

    private void OnApplicationQuit()
    {
        GestorDeEventos.CambiarEscena -= CambiarEscena;
    }
}