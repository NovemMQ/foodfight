using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    //score keeper and game manager singleton 
    private ScoreKeeper ScoreManager;
    private GameManager gameManager;
    [Space(10)]
    [Header("Splats Images")]
    [SerializeField] private Image[] foodVisionImpairmentImageList;
    [SerializeField] private GameObject foodVisionImpairmentImageListObj;
    [SerializeField] private int maxSplatsPerDamange;
    [SerializeField] private float visionImpairedTimer;
    [SerializeField] private float fadeTime;
    private int splatIndex = 0;
    private bool gameOver = false;
    private bool flag1 = true;
    private bool flag2 = true;

    [Space(10)]
    [Header("Splats Audio")]
    [SerializeField] private GameObject splatSoundsListObj;
    private AudioSource[] splatSoundsList;

    public bool GameOver { get => gameOver; set => gameOver = value; }
    public Image[] FoodVisionImpairmentImageList { get => foodVisionImpairmentImageList; }
    private bool playerHasBeenHit = false;
    public bool PlayerHasBeenHit { get => playerHasBeenHit; }
    public float VisionImpairedTimer { get => visionImpairedTimer; }
    public float FadeTime { get => fadeTime; }

    private void Start()
    {
        ScoreManager = FindObjectOfType<ScoreKeeper>();
        gameManager = FindObjectOfType<GameManager>();
        foodVisionImpairmentImageList = GetComponentsInChildren<Image>();
        splatSoundsList = splatSoundsListObj.GetComponentsInChildren<AudioSource>();
    }

    private void Update()
    {
        ManageSplatDisplayPeriod();
    }

    //make sure the splats are only on during game time
    private void ManageSplatDisplayPeriod()
    {
        if (gameManager.GameStart && !gameOver && flag1) //if the game has started and is not game over
        {
            foreach (Image i in foodVisionImpairmentImageList)//turn on the player damaged splats
            {
                i.enabled = true;
            }
            flag1 = false;
        }

        if (gameOver && flag2)//if the game is over and the score ui is on turn off the player damage splats
        {
            foreach (Image i in foodVisionImpairmentImageList)
            {
                i.enabled = false;
            }
            flag2 = false;
        }

    }

    //die when hit by food
    private void OnTriggerEnter(Collider other)
    {
        if (TagManager.CompareTags(other.gameObject, "enemyFood"))
        {
            if (foodVisionImpairmentImageList.Length > 0)
            {
                for (int i = 0; i <= maxSplatsPerDamange; i++)
                {
                    splatIndex = Random.Range(0, foodVisionImpairmentImageList.Length);
                    foodVisionImpairmentImageList[splatIndex].GetComponent<SplatHandler>().SplashImageOn();
                }
            }
            if(splatSoundsList.Length > 0){splatSoundsList[Random.Range(0, splatSoundsList.Length)].Play();}
            ScoreManager.addPlayerGotHit();
            playerHasBeenHit = true;
        }
    }
}
