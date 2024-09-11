using UnityEngine;
using UnityEngine.UI;

public class GestorAvatar : MonoBehaviour
{
    public RawImage Avatar;

    public Sprite AvatarGuerrero;
    public Sprite AvatarTirador;

    public void EstablecerAvatarGuerrero()
    {
        Avatar.texture = AvatarGuerrero.texture;
    }

    public void EstablecerAvatarTirador()
    {
        Avatar.texture = AvatarTirador.texture;
    }
}