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
        Debug.Log("activate start splash screen UI");
        startUI.SetActive(true);
    }
    public void DeactivateStartSplashScreenUI()
    {
        Debug.Log("deactivate start splash screen UI");
        startUI.SetActive(false);
    }

}
