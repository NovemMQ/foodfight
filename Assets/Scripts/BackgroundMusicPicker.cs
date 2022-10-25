using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPicker : MonoBehaviour
{
    private AudioSource[] backgroundMusicList;
    [SerializeField] private int trackNumber = -1;

    private void Start()
    {
        backgroundMusicList = GetComponentsInChildren<AudioSource>();
        if (trackNumber == -1 && backgroundMusicList.Length > 0)
        {
            playTrack(trackNumber);
        }
    }

    private void Update()
    {
        if (trackNumber > -1)
        {
            if (backgroundMusicList[trackNumber].isPlaying == false)
            {
                playTrack(trackNumber);
            }
        }
    }

    private void playTrack(int trackNum)
    {
        trackNumber = Random.Range(0, backgroundMusicList.Length);
        backgroundMusicList[trackNum].Play();
    }
}
