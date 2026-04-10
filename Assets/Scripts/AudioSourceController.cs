
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    public AudioSource _Source;

    // Objects 
    public AudioClip coinSFX;
    public AudioClip deathSFX;
    public AudioClip jumpSFX;
    public AudioClip winSFX;
    
    private void Start()
    {
        _Source = GetComponent<AudioSource>();
    }


    // Retuns the asked for Audio Soruce Game Object 
    public void PlaySFX(string audioName) {
        AudioClip clip = coinSFX; 
        switch (audioName) {
            case Structs.SoundEffects.coin:
                {
                    clip = coinSFX;
                    break;
                }
            case Structs.SoundEffects.death:
                {
                    clip = deathSFX;
                    break;
                }
            case Structs.SoundEffects.jump:
                {
                    clip = jumpSFX;
                    break;
                }
            case Structs.SoundEffects.win:
            {
                clip = winSFX;
                break;
            }
        }
        _Source.PlayOneShot(clip);
    }
}
