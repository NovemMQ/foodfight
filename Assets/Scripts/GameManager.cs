using Liminal.SDK.Core;
using Liminal.Core.Fader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager instance;
    public static GameManager Instance
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

    [Space(10)]
    [Header("Liminal game manager methods")]
    [SerializeField] private ExperienceApp myExperienceApp;

    // holds current game time
    [Space(10)]
    [Header("Game Time")]
    [Space(10)]
    [Tooltip("time in seconds")]
    [SerializeField] private float timeLimit = 180f; //secs
    private float gameTime;
    [Space(10)]
    [SerializeField] private Color fadeColor = Color.black; //end fade colour
    [Tooltip("time in seconds")]
    [SerializeField] private float fadeDuration = 120; //secs

    public float GameTime
    {
        get
        {
            return gameTime;
        }
    }

    void Start(){
        //set up before update
        //set timer = 0
        gameTime = 0;
    }

    void Update() {
        //update timer
        gameTime = Time.timeSinceLevelLoad;
        //if timer is more than time limit, end the game.    
        if(gameTime > timeLimit){
            DisplayScores();
        }
    }

    public void PauseGame()
    {
        myExperienceApp.Pause();
    }

    public void ResumeGame()
    {
        myExperienceApp.Resume();
    }

    public void EndGame()
    {
        // only in UI calles this myExperienceApp.EndExperience();
        FadeScreen();
        ExperienceApp.End();
    }

    private void DisplayScores(){
        //end the game when timer finishes
        //get scores
        GetScores();
;        //launch ending UI, score display
        StartLetterScoreEvent();
        //stop enemy
        // only in UI myExperienceApp.EndExperience();
    }

    private void GetScores(){
        //set the 3 scores here.
    }

   private void StartLetterScoreEvent()
    {
        //launch ending UI event in UI manager
        // send scores to UI manager to display them on the letter
    }

    private void FadeScreen()
    {
        var fader = ScreenFader.Instance;
        fader.FadeTo(fadeColor, fadeDuration);
    }


}