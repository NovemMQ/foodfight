using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schoolBellScript : MonoBehaviour
{
    private AudioSource soundAudio;
    private AudioManager audioManger;
    private float duration = 5f;//secs
    private float durationCounter = 0f;
    private float elapsedTime = 0f;
    private float fadeTime = 2f;

    public float Duration { get => duration; set => duration = value; }
    public float FadeTime { get => fadeTime; set => fadeTime = value; }

    private bool volumeFaded = false;
    // Start is called before the first frame update
    void Start()
    {
        audioManger = FindObjectOfType<AudioManager>();
        soundAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //make sure to reset the volume to 1 when the sound finished playing
        if (soundAudio.volume != 1f && volumeFaded)
        {
            soundAudio.volume = 1f;
            volumeFaded = false;
        }

        //if the sound is playing, only play the sound for the duration time and fade the volume to 0
        if (soundAudio.isPlaying)
        {
            durationCounter += Time.deltaTime;
            if(durationCounter >= duration)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime < fadeTime)
                {
                    audioManger.SetAudioFadeandStop(soundAudio, elapsedTime, fadeTime);
                }
                else
                {
                    // when the while-loop has ended, the audiolistener volume should be 0 and the screen completely black. However, for safety's sake, we should manually set AudioListener volume to 0.
                    soundAudio.volume = 0f;
                    soundAudio.Stop();
                    durationCounter = 0;
                    elapsedTime = 0;
                    volumeFaded = true;
                }
     
            }
        }
    }
}
