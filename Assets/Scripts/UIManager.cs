using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startCountDownText;
    [SerializeField] private string startCounterStringFormat = "{0}";
    [SerializeField] private GameObject startUI; //start splash screen ui panel
    [SerializeField] private TextMeshProUGUI foodThrownText;
    [SerializeField] private TextMeshProUGUI enemiesDiedText;
    [SerializeField] private TextMeshProUGUI playerDamagedText;
    [SerializeField] private GameObject scoreUI; //ending ui panel
    [SerializeField] private TextMeshProUGUI endingCountDownText;
    [SerializeField] private string endingCounterStringFormat = "Ending in {0} Seconds ...";
    [SerializeField] private GameObject gametimeCountdownUI; //ending ui panel
    [SerializeField] private TextMeshProUGUI gametimeCountdownText;
    [SerializeField] private string gametimeCountdownStringFormat = "{0}:{1}";

    private float minInSec = 60f;
    private GameManager gameManager;
   // public delegate void UIUpdate();
   // public static event UIUpdate UIUpdateFood;

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
        endingCounterStringFormat = endingCountDownText.text;
        startCounterStringFormat = startCountDownText.text;
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

    public void SetScoreUIText(int food, int enemy, int player)
    {
        SetFoodThrownUI(food);
        SetEnemiesDefeatedUI(enemy);
        SetPlayerDamagedUI(player);
    }

    public void SetFoodThrownUI(int foodThrown)
    {
        Debug.Log("food thrown here "+ foodThrown);
        foodThrownText.text = string.Format(foodThrownText.text, foodThrown.ToString());
    }

    public void SetEnemiesDefeatedUI(int enemyDied)
    {
        Debug.Log("enemy died here " + enemyDied);
        enemiesDiedText.text = string.Format(enemiesDiedText.text, enemyDied.ToString());
    }

    public void SetPlayerDamagedUI(int playerDamaged)
    {
        Debug.Log("player damaged here " + playerDamaged);
        playerDamagedText.text = string.Format(playerDamagedText.text, playerDamaged.ToString());
    }

    public void ActivateScoreUI()
    {
        Debug.Log("activate score UI");
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
