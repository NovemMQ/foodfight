using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPicker : MonoBehaviour
{
    [SerializeField]private AudioSource[] backgroundMusicList;
    private AudioManager audioManger;
    [SerializeField] private int trackNumber = -1;
    private GameManager gameManager;
    private float elapsedTime = 0f;
    private float fadeTime = 2f;
    private AudioSource backGroundAudio;

    public AudioSource BackGroundAudio { get => backGroundAudio; set => backGroundAudio = value; }

    private void Start()
    {
        audioManger = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        backgroundMusicList = GetComponentsInChildren<AudioSource>();
        if (trackNumber == -1 && backgroundMusicList.Length > 0)
        {
            playTrack(trackNumber);
        }
        backGroundAudio = backgroundMusicList[trackNumber];
        backgroundMusicList[trackNumber].volume = 0;
    }

    private void Update()
    {
        /* if (trackNumber > -1)
         {
             if (backgroundMusicList[trackNumber].isPlaying == false)
             {
                 playTrack(trackNumber);
             }
         }*/

        if (backgroundMusicList[trackNumber].isPlaying)
        {
            Debug.Log("fade here is !!!!!!" + elapsedTime);

            elapsedTime += Time.deltaTime;

            if (elapsedTime < fadeTime)
            {
                Debug.Log("fade here is !!!!!! --- " + elapsedTime);

                audioManger.SetAudioFadeIn(backgroundMusicList[trackNumber], elapsedTime, fadeTime);

            }
        }
            
        
        
    }

    private void playTrack(int trackNum)
    {
        trackNumber = Random.Range(0, backgroundMusicList.Length);
        backgroundMusicList[trackNum].Play();
    }
}
