using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private GameObject gigglingListObj;
    [SerializeField] private AudioSource[] gigglingList;
    [SerializeField] private GameObject dyingListObj;
    [SerializeField] private AudioSource[] dyingList;
    private int giggleTrackNumber = -1;
    private int diedTrackNumber = -1;
    [SerializeField] private float randomPlayTimerMax;//sec
    [SerializeField] private float randomPlayTimerMin;//sec
    private float randomePlayCounter;

    // Start is called before the first frame update
    void Start()
    {
        gigglingList = gigglingListObj.GetComponentsInChildren<AudioSource>();
        dyingList = dyingListObj.GetComponentsInChildren<AudioSource>();
        PickRandomTrack(gigglingList);
        PickRandomTrack(dyingList);
        SetRandomPlayCounter();
    }

    // Update is called once per frame
    void Update()
    {
        ManageGigglingTrackPlayRate();
    }

    //play the sound when counter is finished
    private void ManageGigglingTrackPlayRate()
    {
        randomePlayCounter -= Time.deltaTime;
        if (giggleTrackNumber > -1)
        {
            if (gigglingList[giggleTrackNumber].isPlaying == false && randomePlayCounter <= 0f)
            {
                gigglingList[giggleTrackNumber].Play();
                SetRandomPlayCounter();
            }
        }
    }

    public void PlayDyingSound()
    {
        dyingList[diedTrackNumber].Play();
        PickRandomTrack(dyingList);
    }

    //pick a random sound track for this enemy to play
    private void PickRandomTrack(AudioSource[] audioList)
    {
        if (audioList != null || audioList.Length > 0)
        {
            SetRandomTrack(audioList);
        }
    }


    //randomly set the play counter time for the next time the sound plays
    private void SetRandomPlayCounter()
    {
        randomePlayCounter = Random.Range(randomPlayTimerMin, randomPlayTimerMax);
    }

    //get the random track number
    private void SetRandomTrack(AudioSource [] audioList)
    {
        if (audioList.Equals(gigglingList))
        {
            giggleTrackNumber = Random.Range(0, audioList.Length);
        } else if (audioList.Equals(dyingList))
        {
            diedTrackNumber = Random.Range(0, audioList.Length);
        }
    }
}
