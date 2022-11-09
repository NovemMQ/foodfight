using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private ScoreKeeper ScoreManager;
    [SerializeField] private Image[] foodVisionImpairmentImageList;
    [SerializeField] private GameObject foodVisionImpairmentImageListObj;
    [SerializeField] private int maxSplatsPerDamange;
    [SerializeField] private float visionImpairedTimer;
    [SerializeField] private float fadeTime;
    private GameManager gameManager;
    private int splatIndex = 0;
    private bool gameOver = false;
    private bool flag1 = true;
    private bool flag2 = true;

    public bool GameOver { get => gameOver; set => gameOver = value; }
    public Image[] FoodVisionImpairmentImageList { get => foodVisionImpairmentImageList; set => foodVisionImpairmentImageList = value; }

    private bool playerHasBeenHit = false;

    public bool PlayerHasBeenHit { get => playerHasBeenHit; }
    public float VisionImpairedTimer { get => visionImpairedTimer; }
    public float FadeTime { get => fadeTime; }

    private void Start()
    {
        ScoreManager = FindObjectOfType<ScoreKeeper>();
        foodVisionImpairmentImageList = GetComponentsInChildren<Image>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(gameManager.GameStart && !gameOver && flag1)
        {
            foreach(Image i in foodVisionImpairmentImageList)
            {
                i.enabled = true;
            }
            flag1 = false;
        }

        if (gameOver && flag2)
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
            for(int i=0; i<=maxSplatsPerDamange; i++)
            {
                splatIndex = Random.Range(0, foodVisionImpairmentImageList.Length);
                foodVisionImpairmentImageList[splatIndex].GetComponent<SplatHandler>().SplashImageOn();
            }
          
            ScoreManager.addPlayerGotHit();
            playerHasBeenHit = true;
        }
    }
}
