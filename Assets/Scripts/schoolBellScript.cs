﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schoolBellScript : MonoBehaviour
{
    private AudioSource soundAudio;
    private AudioManager audioManger;
    [SerializeField]private float duration = 5f;//secs
    private float durationCounter = 0f;
    private float elapsedTime = 0f;
   [SerializeField] private float fadeTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        audioManger = FindObjectOfType<AudioManager>();
        soundAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (soundAudio.volume != 1f)
        {
            soundAudio.volume = 1f;
        }

        if (soundAudio.isPlaying)
        {
            durationCounter += Time.deltaTime;
            if(durationCounter >= duration)
            {
                Debug.Log("bell is playing here");
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
                }
     
            }
        }
    }
}
