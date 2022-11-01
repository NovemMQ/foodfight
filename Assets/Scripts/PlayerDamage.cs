using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private ScoreKeeper ScoreManager;
    [SerializeField] private Image foodVisionImpairmentImage;
    [SerializeField] private float visionImpairedTimer; //sec how long the image stays on for
    private float visionImpairedCounter;
    [SerializeField] private float fadeTime;
    private float elapsedTime; //instantiate a float with a value of 0 for use as a timer.
    [SerializeField] private float startingAlpha;

    private bool gameOver = false;

    public bool GameOver { get => gameOver; set => gameOver = value; }
    public Image FoodVisionImpairmentImage { get => foodVisionImpairmentImage; set => foodVisionImpairmentImage = value; }

    private bool playerHasBeenHit = false;


    private void Start()
    {
        ScoreManager = FindObjectOfType<ScoreKeeper>();
        foodVisionImpairmentImage.color = new Color(foodVisionImpairmentImage.color.r, foodVisionImpairmentImage.color.g, foodVisionImpairmentImage.color.b, 0);
        visionImpairedCounter = -1f;
        elapsedTime = 0f;
        startingAlpha /= 255f;
    }

    private void Update()
    {
        if (!playerHasBeenHit)
        {
            foodVisionImpairmentImage.color = new Color(foodVisionImpairmentImage.color.r, foodVisionImpairmentImage.color.g, foodVisionImpairmentImage.color.b, 0);
        }
        visionImpairedCounter -= Time.deltaTime;
        if (visionImpairedCounter <= 0)
        {
            //foodVisionImpairmentImage.enabled = false;
           
            if (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime; // Count up
                foodVisionImpairmentImage.color = new Color(foodVisionImpairmentImage.color.r, foodVisionImpairmentImage.color.g, foodVisionImpairmentImage.color.b, Mathf.Lerp(startingAlpha, 0f, elapsedTime / fadeTime));
            }
        }

        if (foodVisionImpairmentImage.color.a == 0f)
            {
                if (gameOver)
                {
                foodVisionImpairmentImage.enabled = false;
                }
                if (playerHasBeenHit)
                {
                    playerHasBeenHit = false;
                }
            }
   
    }
    //die when hit by food
 
    private void OnTriggerEnter(Collider other)
    {
        if (TagManager.CompareTags(other.gameObject, "enemyFood"))
        {
            elapsedTime = 0f;
            visionImpairedCounter = visionImpairedTimer;
            //foodVisionImpairmentImage.enabled = true;
            foodVisionImpairmentImage.color = new Color(foodVisionImpairmentImage.color.r, foodVisionImpairmentImage.color.g, foodVisionImpairmentImage.color.b, startingAlpha);
            ScoreManager.addPlayerGotHit();
            playerHasBeenHit = true;
        }
    }
}
