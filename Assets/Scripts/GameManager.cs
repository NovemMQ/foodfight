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
    [SerializeField] private float gameTime = -1f;
    [SerializeField] private float endingUITimer = 10;//secs
    private float endingUICounter;
    public float GameTime {get { return gameTime; }}
    //UI 
    private UIManager uiManager;
    [SerializeField] private float startUITimer = 5;//secs
    public float StartUITimer { get => startUITimer; set => startUITimer = value; }
    private float startUICounter;
    private bool startGameUIOn = true;
    private bool gameOverEndingUIOn = false;
    private float gameCountdown = 0;
    private bool gameStart = false;
    private bool gameOverFlag = true;
    public bool GameOverEndingUIOn { get => gameOverEndingUIOn; set => gameOverEndingUIOn = value; }
    public bool GameStart { get => gameStart; set => gameStart = value; }
    public float TimeLimit { get => timeLimit; }
    //enemy movement manager
    private enemyMovementManager enemyManager;
    //score
    private ScoreKeeper scorekeeper;
    private int foodThrown =3;
    private int enemyDeath;
    private int playerGotHit;

    //audio manager
    private AudioManager audioManger;
    private float minSec = 50f;//75 secs
    private bool played1MinBell = true;
    [Space(10)]
    [Header("School Bell Time")]
    [SerializeField] private float startSchoolBellDuration = 4f;
    [SerializeField] private float startSchoolBellFadeOutDuration = 2f;
    [SerializeField] private float oneMinSchoolBellDuration = 1f;
    [SerializeField] private float oneMinSchoolBellFadeOutDuration = 0.5f;

    //player's head, playerdamaged script
    private PlayerDamage playerDamageScript;

    void Start(){
        //set up before update
        //get singleton managers
        enemyManager = FindObjectOfType<enemyMovementManager>();
        uiManager = FindObjectOfType<UIManager>();
        scorekeeper = FindObjectOfType<ScoreKeeper>();
        audioManger = FindObjectOfType<AudioManager>();
        myExperienceApp = FindObjectOfType<MyExperienceApp>();
        //player damage
        playerDamageScript = FindObjectOfType<PlayerDamage>();
        //set up game time counters
        startUICounter = StartUITimer;
        endingUICounter = endingUITimer;
        //start start log UI, and 3 sec counter
        uiManager.ActivateStartSplashScreenUI();
        uiManager.StartGametimeCounter(timeLimit);
        //enemy don't move yet, before 3 seconds, now they move straight away because they take long to walk into the scene anyway
        //enemyManager.StopEnemyWavesMovement(); // left here to uncomment if decided to only have the enemy move when dame starts after 3 sec, if uncommented you need to also uncomment enemyManager.StartEnemyWavesMovement() in SetGameUp();
        //school bell rings 3 times in the game
        minSec = timeLimit / 3;
        //audio
        audioManger.PlayCountdownBeep();
    }

    void Update() {

        //start the game UI begining splash screen
        if (startGameUIOn)// if the start logo is on display
        {
            startUICounter -= Time.deltaTime;
            uiManager.StartStartCounter(startUICounter); //send counter to UI
            SetCurrentScoreInGame();//reset current score displayed on laptoop to zero
            if (startUICounter <= 0f) //turn off start splash screen
            {
                StartSetGameUp();    
            }
        }
        else
        {
            //update timer
            gameTime += Time.deltaTime;
            GameCountdownUIManger(gameTime);//updates the current game time displayed on the digital clock
            GameCurrentScoreDisplayManager();//updates the current score displayed on the laptop
            if ((int)gameTime == minSec) //after the fist bell long duration play the short bell duration
            {
                played1MinBell = false;
            }
        }
        PlaySchoolBellsSchdule();
        ManageEndGame();
    }
    
    private void StartSetGameUp()
    {
        startGameUIOn = false;
        uiManager.DeactivateStartSplashScreenUI();
        //enemyManager.StartEnemyWavesMovement(); //enemy movement in the start is not stopped/uncommented so we don't need to start the enemy movement, they are already moving
        audioManger.PlaySchoolBell(startSchoolBellDuration, startSchoolBellFadeOutDuration);
        audioManger.PlayBackgroundMusic();
        gameTime = 0;
        played1MinBell = true;
        gameStart = true;
    }

    //updates the current game time displayed on the digital clock
    private void GameCountdownUIManger(float currentGameTime)
    {
        gameCountdown = currentGameTime;
        if (gameTime > timeLimit)
        {
            gameCountdown = timeLimit;
        }
        uiManager.StartGametimeCounter(timeLimit - gameCountdown);
    }

    //updates the current score displayed on the laptop
    private void GameCurrentScoreDisplayManager()
    {
        if (!gameOverEndingUIOn)
        {
            SetCurrentScoreInGame();
        }
    }

    private void SetCurrentScoreInGame()
    {
        setScores();
        uiManager.SetCurrentScoreText(foodThrown, enemyDeath, playerGotHit);
    }

    //end the game when timer finishes
    //get scores
    //launch ending UI, score display
    //stop enemy pause game
    // myExperienceApp.IsEndGame = true;
    // only in UI myExperienceApp.EndExperience();
    private void ManageEndGame()
    {

        //if timer is more than time limit, end the game.    
        if (gameTime > timeLimit && gameOverFlag)
        {
            //gameTime = -1000000;
            StartLetterScoreEvent();// call UImanager for score display
            gameOverEndingUIOn = true;
            gameOverFlag = false;
        }

        if (gameOverEndingUIOn)
        {
            endingUICounter -= Time.deltaTime;
            uiManager.StartEndingCounter(endingUICounter);
            gameStart = false;
            playerDamageScript.GameOver = true;
        }

        if (endingUICounter <= 0f && gameOverEndingUIOn)
        {
            gameOverEndingUIOn = false;
            EndGame();
        }
    }
    private void PlaySchoolBellsSchdule()
    {
        
        if ((((int)gameTime) % minSec) == 0)
        {
            if (!played1MinBell)
            {
                played1MinBell = true;
                if((int)gameTime == timeLimit)
                {
                    oneMinSchoolBellDuration = startSchoolBellDuration;
                    oneMinSchoolBellFadeOutDuration = startSchoolBellFadeOutDuration;
                }
                audioManger.PlaySchoolBell(oneMinSchoolBellDuration, oneMinSchoolBellFadeOutDuration);
            }
        }
        else
        {
            if (played1MinBell)
            {
                played1MinBell = false;
            }
        }
    }

    public void EndGame(){
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
        float accuracy = 0;
        if (foodThrown != 0) { 
            accuracy = (int)(((float)enemyDeath / (float)foodThrown) * 10000);
            accuracy /= 100;
        }

        uiManager.SetScoreUIText(accuracy, enemyDeath, playerGotHit);
    }
    
   private void StartLetterScoreEvent()
    {
        //launch ending UI event in UI manager
        // send scores to UI manager to display them on the letter
        sendScoresToUI(); // get the scores form scorekeeper
        uiManager.ActivateScoreUI(); // turns on score UI
        audioManger.PlayReportCardPaperSound();
        //game will fade and exit after scoreUI pops up and countdown in the display finishes 
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