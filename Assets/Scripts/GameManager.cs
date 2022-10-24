using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.Core.Fader;
using Liminal.SDK.Core;
using Liminal.Experience;

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
    private MyExperienceApp myExperienceApp;

    // holds current game time
    [Space(10)]
    [Header("Game Time")]
    [TextArea]
    public string Notes = "time in seconds, do not use 'dontDestoryonLoad'";
    [Space(10)]
    [Tooltip("time in seconds")]
    [SerializeField] private float timeLimit = 180f; //secs
    [SerializeField] private float gameTime = 0f;
    [SerializeField] private float endingUITimer = 10;//secs
    private float endingUICounter;
    public float GameTime {get { return gameTime; }}
    private bool gameOverEndingUIOn = false;
    public bool GameOverEndingUIOn { get => gameOverEndingUIOn; set => gameOverEndingUIOn = value; }
   


    //UI
    private UIManager uiManager;
    //score
    private ScoreKeeper scorekeeper;
    private int foodThrown;
    private int enemyDeath;
    private int playerGotHit;

    void Start(){
        //set up before update
        uiManager = FindObjectOfType<UIManager>();
        scorekeeper = FindObjectOfType<ScoreKeeper>();
        myExperienceApp = FindObjectOfType<MyExperienceApp>();
        endingUICounter = endingUITimer;
    }

    void Update() {
        //update timer
        gameTime += Time.deltaTime;
        //if timer is more than time limit, end the game.    
        if(gameTime > timeLimit){
            gameTime = -1000000;
            StartLetterScoreEvent();// call UImanager for score display
            gameOverEndingUIOn = true;
        }

        if(gameOverEndingUIOn)
        {
            endingUICounter -= Time.deltaTime;
            uiManager.StartEndingCounter(endingUICounter);
        }

        if (endingUICounter <= 0f && gameOverEndingUIOn)
        {
            gameOverEndingUIOn = false;
            EndGame();
        }
    }

    public void PauseGame()
    {
        //not using liminal's pausegame
        //turn off enemy and player launcher
    }

    public void ResumeGame()
    {
        
    }

    public void EndGame(){
        //end the game when timer finishes
        //get scores
        //launch ending UI, score display
        //stop enemy pause game
       // myExperienceApp.IsEndGame = true;
        // only in UI myExperienceApp.EndExperience();
        Debug.Log("end game now!");
        StartCoroutine(FadeAndExit(2f));
    }
    
    public void addScore()
    {
        scorekeeper.addFoodThrown();
    }

    public void setScores(){
        //set the 3 scores here.
        foodThrown = scorekeeper.FoodThrown;
        enemyDeath = scorekeeper.EnemyDeath;
        playerGotHit = scorekeeper.PlayerGotHit;
    }

    //grab the scorekeeper data and send to Ui
    private void sendScoresToUI()
    {
        setScores();//get and set the score from score keeper
        //UI manager score display ui method
        uiManager.SetScoreUIText(foodThrown, enemyDeath, playerGotHit);
    }
    
   private void StartLetterScoreEvent()
    {
        Debug.Log("starting the score UI event");
        //launch ending UI event in UI manager
        // send scores to UI manager to display them on the letter
        sendScoresToUI(); // get the scores form scorekeeper
        uiManager.ActivateScoreUI(); // turns on score UI
        PauseGame(); //pause the game
        //end game button in the menu will fade and exit the game.
    }

 

    // This coroutine fades the camera and audio simultaneously over the same length of time.
    IEnumerator FadeAndExit(float fadeTime)
    {
        Debug.Log("in fade and exit");
        var elapsedTime = 0f; //instantiate a float with a value of 0 for use as a timer.
        var startingVolume = AudioListener.volume; //this gets the current volume of the audio listener so that we can fade it to 0 over time.

        ScreenFader.Instance.FadeTo(Color.black, fadeTime); // Tell the system to fade the camera to black over X seconds where X is the value of fadeTime.

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime; // Count up
            AudioListener.volume = Mathf.Lerp(startingVolume, 0f, elapsedTime / fadeTime); // This uses linear interpolation to change the volume of AudioListener over time.
            yield return new WaitForEndOfFrame(); // Tell the coroutine to wait for a frame to avoid completing this loop in a single frame.
        }

        // when the while-loop has ended, the audiolistener volume should be 0 and the screen completely black. However, for safety's sake, we should manually set AudioListener volume to 0.
        AudioListener.volume = 0f;

        ExperienceApp.End(); // This tells the platform to exit the experience.
        //MyExperienceApp.EndExperience();

    }
}