using Liminal.SDK.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    #region singleton
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

    #endregion

    [Space(10)]
    [Header("Liminal game manager methods")]
    [SerializeField] private ExperienceApp myExperienceApp;

    // holds current game time
    [Space(10)]
    [Header("Game Time")]
    [TextArea]
    public string Notes = "time in seconds, do not use 'dontDestoryonLoad'";
    [Space(10)]
    [Tooltip("time in seconds")]
    [SerializeField] private float timeLimit = 180f; //secs
    private float gameTime;
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
    }

    void Update() {
        //update timer
        gameTime = Time.timeSinceLevelLoad;
        //if timer is more than time limit, end the game.    
        if(gameTime > timeLimit){
            endGame();
        }
    }

    public void pauseGame()
    {
        myExperienceApp.Pause();
    }

    public void resumeGame()
    {
        myExperienceApp.Resume();
    }

    public void endGame(){
        //end the game when timer finishes
        //get scores
        //launch ending UI, score display
        //stop enemy
        // only in UI myExperienceApp.EndExperience();
    }

    private void getScores(){
        //set the 3 scores here.
    }

   private void startLetterScoreEvent()
    {
        //launch ending UI event in UI manager
        // send scores to UI manager to display them on the letter
    }

}