using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private GameObject soundListObj;
    [SerializeField] private AudioSource[] soundList;
    private int trackNumber = -1;
    [SerializeField] private float randomPlayTimerMax;//sec
    [SerializeField] private float randomPlayTimerMin;//sec
    private float randomePlayCounter;

    // Start is called before the first frame update
    void Start()
    {
        soundList = soundListObj.GetComponentsInChildren<AudioSource>();
        if (soundList != null || soundList.Length > 0)
        {
            SetRandomTrack();
        }
        SetRandomPlayCounter();
    }

    // Update is called once per frame
    void Update()
    { 
        randomePlayCounter -= Time.deltaTime;
        if (trackNumber > -1)
        {
            if(soundList[trackNumber].isPlaying == false && randomePlayCounter <= 0f)
            {
                soundList[trackNumber].Play();
                SetRandomPlayCounter();
            }
        }

    }

    private void SetRandomPlayCounter()
    {
        randomePlayCounter = Random.Range(randomPlayTimerMin, randomPlayTimerMax);
    }

    private void SetRandomTrack()
    {
        trackNumber = Random.Range(0, soundList.Length);
    }
}
