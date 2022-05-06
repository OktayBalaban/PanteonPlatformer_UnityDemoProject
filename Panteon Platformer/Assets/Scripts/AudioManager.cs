using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource stageMusic;
    public AudioSource victoryMusic;

    // Start is called before the first frame update
    void Start()
    {
        stageMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMusic()
    {
        stageMusic.Stop();
        victoryMusic.Play();
    }
}
