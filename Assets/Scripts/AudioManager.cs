using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region singleton
    //Singleton
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no GameManger instance in the scene");
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            //destroy duplicates
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    [SerializeField] private AudioSource schoolBell;
    [SerializeField] private AudioSource countdownBeep;
    [SerializeField] private AudioSource reportCardPaperSound;
    [SerializeField] private BackgroundMusicPicker background;

    public void SetAudioFadeandStop(AudioSource soundAudio, float elapsedTime, float fadeTime)
    {
        var startingVolume = soundAudio.volume; //this gets the current volume of the audio listener so that we can fade it to 0 over time.
        soundAudio.volume = Mathf.Lerp(startingVolume, 0f, elapsedTime / fadeTime); // This uses linear interpolation to change the volume of AudioListener over time.
    }

    public void SetAudioFadeIn(AudioSource soundAudio, float elapsedTime, float fadeTime)
    {
        var startingVolume = soundAudio.volume; //this gets the current volume of the audio listener so that we can fade it to 0 over time.
        soundAudio.volume = Mathf.Lerp(startingVolume, 1f, elapsedTime / fadeTime); // This uses linear interpolation to change the volume of AudioListener over time.
    }

    //used when the experience first loads during the start logo UI
    public void PlayCountdownBeep()
    {
        countdownBeep.Play();
    }

    public void PlaySchoolBell(float duration, float fadeTime)
    {
        schoolBell.Play();
        schoolBell.GetComponent<schoolBellScript>().Duration = duration;
        schoolBell.GetComponent<schoolBellScript>().FadeTime = fadeTime;
    }

    public void PlayReportCardPaperSound()
    {
        reportCardPaperSound.Play();
    }

    public void PlayBackgroundMusic()
    {
        background.BackGroundAudio.Play();
    }
}
