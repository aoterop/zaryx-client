using UnityEngine;

public class GestorAnimaciones : MonoBehaviour
{
    private Animator animator;

    public RuntimeAnimatorController controlador_guerrero;
    public RuntimeAnimatorController controlador_tirador;
    public RuntimeAnimatorController controlador_mago;

    private byte _clase;


    public void EstablecerAnimator(byte clase)
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        _clase = clase;
        switch(clase)
        {
            case (byte)Tipos.Clase.GUERRERO:
                {
                    animator.runtimeAnimatorController = controlador_guerrero;
                }break;

            case (byte)Tipos.Clase.TIRADOR:
                {
                    animator.runtimeAnimatorController = controlador_tirador;
                }
                break;

            case (byte)Tipos.Clase.MAGO:
                {
                    animator.runtimeAnimatorController = controlador_mago;
                }
                break;

            default: { } break;
        }
    }

    public void Play(byte animacion)
    {
        switch (_clase)
        {
            case (byte)Tipos.Clase.GUERRERO:
                {
                    switch(animacion)
                    {
                        case (byte)Tipos.Animaciones.IDLE:
                            {
                                animator.Play("Idle_SwordShield");
                            }break;

                        case (byte)Tipos.Animaciones.MOVIMIENTO:
                            {
                                animator.Play("Run_SwordShield");
                            }break;

                        default: { } break;
                    }
                }
                break;

            case (byte)Tipos.Clase.TIRADOR:
                {
                    switch (animacion)
                    {
                        case (byte)Tipos.Animaciones.IDLE:
                            {
                                animator.Play("Idle_Bow");
                            }
                            break;

                        case (byte)Tipos.Animaciones.MOVIMIENTO:
                            {
                                animator.Play("Run_Bow");
                            }
                            break;

                        default: { } break;
                    }
                }
                break;

            case (byte)Tipos.Clase.MAGO:
                {
                    switch (animacion)
                    {
                        case (byte)Tipos.Animaciones.IDLE:
                            {
                                animator.Play("Idle_MagicWand");
                            }
                            break;

                        case (byte)Tipos.Animaciones.MOVIMIENTO:
                            {
                                animator.Play("Run_MagicWand");
                            }
                            break;

                        default: { } break;
                    }
                }
                break;

            default: { } break;
        }
    }
}