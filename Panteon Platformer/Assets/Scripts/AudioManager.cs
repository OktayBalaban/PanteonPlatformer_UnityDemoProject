using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource stageMusic;
    public AudioSource victoryMusic;
    public AudioSource spraySound;

    public bool isSprayPlaying;

    void Start()
    {
        stageMusic.Play();
        isSprayPlaying = false;
    }

    public void SwitchMusic()
    {
        stageMusic.Stop();
        victoryMusic.Play();
    }

    public void playSpraySound()
    {
        spraySound.Play();
        isSprayPlaying = true;
    }

    public void stopSpraySound()
    {
        spraySound.Stop();
        isSprayPlaying = false;
    }
}
