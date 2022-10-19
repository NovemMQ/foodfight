using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI foodThrownText;
    [SerializeField] private TextMeshProUGUI enemiesDiedText;
    [SerializeField] private TextMeshProUGUI playerDamagedText;
    [SerializeField] private GameObject scoreUI; //ending ui panel
    [SerializeField] private TextMeshProUGUI endingCountDownText;
    [SerializeField] private string endingCounterStringFormat = "Ending in {0} Seconds ...";
    [SerializeField] private float endingUITimer = 10;//secs
    private float minInSec = 60f;
    private float endingUICounter;
    private GameManager gameManager;
    public delegate void UIUpdate();
    public static event UIUpdate UIUpdateFood;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        endingUICounter = endingUITimer;
    }

    private void Update()
    {
        endingUICounter -= Time.deltaTime;
        int seconds = Mathf.CeilToInt(endingUICounter % minInSec);
        endingCountDownText.text = string.Format(endingCounterStringFormat, seconds);


        if (endingUICounter <= 0f)
        {
            gameManager.EndGame();
        }
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

}
