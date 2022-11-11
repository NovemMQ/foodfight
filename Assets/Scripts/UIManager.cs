using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Space(10)]
    [Header("Game countdown text")]
    [SerializeField] private GameObject startUI; //start splash screen ui panel
    [SerializeField] private TextMeshProUGUI startCountDownText;
    [SerializeField] private string startCounterStringFormat = "{0}";
    [SerializeField] private GameObject gametimeCountdownUI; //ending ui panel
    [SerializeField] private TextMeshProUGUI gametimeCountdownText;
    [SerializeField] private string gametimeCountdownStringFormat = "{0}:{1}";
    //current in game UI score text on Laptop
    [Space(10)]
    [Header("Current in game score text")]
    [SerializeField] private TextMeshProUGUI foodThrownTextLaptop;
    [SerializeField] private TextMeshProUGUI enemiesDiedTextLaptop;
    [SerializeField] private TextMeshProUGUI playerDamagedTextLaptop;
    private string foodThrownTextLaptopFormat;
    private string enemiesDiedTextLaptopFormat;
    private string playerDamagedTextLaptopFormat;
    //ending UI score text 
    [Space(10)]
    [Header("Ending score UI text")]
    [SerializeField] private TextMeshProUGUI foodThrownText;
    [SerializeField] private TextMeshProUGUI enemiesDiedText;
    [SerializeField] private TextMeshProUGUI playerDamagedText;
    [SerializeField] private GameObject scoreUI; //ending ui panel
    [SerializeField] private TextMeshProUGUI endingCountDownText;
    [SerializeField] private string endingCounterStringFormat = "Ending in {0} Seconds ...";
    

    private float minInSec = 60f;
    private GameManager gameManager;

    #region singleton
    //Singleton
    private static UIManager instance;
    public static UIManager Instance
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

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //set counter string format
        endingCounterStringFormat = endingCountDownText.text;
        startCounterStringFormat = startCountDownText.text;
        //current in game score UI on laptop string format
        foodThrownTextLaptopFormat = foodThrownTextLaptop.text;
        enemiesDiedTextLaptopFormat = enemiesDiedTextLaptop.text;
        playerDamagedTextLaptopFormat = playerDamagedTextLaptop.text;
    }

    public void StartStartCounter(float time)
    {
        int seconds = Mathf.CeilToInt(time % minInSec);
        startCountDownText.text = string.Format(startCounterStringFormat, seconds);
    }

    public void StartEndingCounter(float time)
    {
        int seconds = Mathf.CeilToInt(time % minInSec);
        endingCountDownText.text = string.Format(endingCounterStringFormat, seconds);
    }

    public void StartGametimeCounter(float time)
    {
        int minutes = Mathf.FloorToInt(time / minInSec);
        int seconds = Mathf.FloorToInt(time % minInSec);
        string minText = "{0}";
        string secText = "{1}";
        if (minutes<10)
        {
            minText = "0{0}";
        }
        if (seconds < 10)
        {
            secText = "0{1}";
        }
        gametimeCountdownStringFormat = minText + ":" + secText;
        gametimeCountdownText.text = string.Format(gametimeCountdownStringFormat, minutes, seconds);
    }

    //current UI score text displayed during in game on the laptop
    public void SetCurrentScoreText(float food, int enemy, int player)
    {
        SetFoodThrownUILaptop(food);
        SetEnemiesDefeatedUILaptop(enemy);
        SetPlayerDamagedUILaptop(player);
    }
        public void SetFoodThrownUILaptop(float foodThrown)
    {
        foodThrownTextLaptop.text = string.Format(foodThrownTextLaptopFormat, foodThrown.ToString());
    }
    public void SetEnemiesDefeatedUILaptop(float enemyDied)
    {
        enemiesDiedTextLaptop.text = string.Format(enemiesDiedTextLaptopFormat, enemyDied.ToString());
    }
    public void SetPlayerDamagedUILaptop(float playerDamaged)
    {
        playerDamagedTextLaptop.text = string.Format(playerDamagedTextLaptopFormat, playerDamaged.ToString());
    }

    //ending UI Score text total
    public void SetScoreUIText(float food, int enemy, int player)
    {
        SetFoodThrownUI(food);
        SetEnemiesDefeatedUI(enemy);
        SetPlayerDamagedUI(player);
    }

    public void SetFoodThrownUI(float foodThrown)
    {
        foodThrownText.text = string.Format(foodThrownText.text, foodThrown.ToString());
    }

    public void SetEnemiesDefeatedUI(int enemyDied)
    {
        enemiesDiedText.text = string.Format(enemiesDiedText.text, enemyDied.ToString());
    }

    public void SetPlayerDamagedUI(int playerDamaged)
    {
        playerDamagedText.text = string.Format(playerDamagedText.text, playerDamaged.ToString());
    }

    public void ActivateScoreUI()
    {
        scoreUI.SetActive(true);
    }

    public void ActivateStartSplashScreenUI()
    {
        startUI.SetActive(true);
    }
    public void DeactivateStartSplashScreenUI()
    {
        startUI.SetActive(false);
    }

}
