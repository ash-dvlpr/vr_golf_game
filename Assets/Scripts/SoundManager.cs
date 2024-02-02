using UnityEngine;

//Clase para almacenar audios que serán accesibles desde cualquier clase
public class SoundManager : MonoBehaviour
{
    public static SoundManager sharedInstance;

    public AudioSource gameMusic;
    public AudioSource endGameMusic;
    public AudioSource clappingSound;
    public AudioSource trumpetsSound;
    
    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(this);
        }
        else
        {
            sharedInstance = this;
        }
    }
}
