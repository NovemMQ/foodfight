using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplatHandler : MonoBehaviour
{
    [SerializeField] private Image foodVisionImpairmentImage;
    [SerializeField] private float visionImpairedTimer; //sec how long the image stays on for
    private float visionImpairedCounter;
    [SerializeField] private float fadeTime;
    private float elapsedTime; //instantiate a float with a value of 0 for use as a timer.
    private float startingAlpha;
    private PlayerDamage playerDamage;
    public Image FoodVisionImpairmentImage { get => foodVisionImpairmentImage; set => foodVisionImpairmentImage = value; }

    void Start()
    {
        playerDamage = FindObjectOfType<PlayerDamage>();
        foodVisionImpairmentImage = GetComponentInChildren<Image>();
        startingAlpha = 1f;
        //foodVisionImpairmentImage.color = new Color(foodVisionImpairmentImage.color.r, foodVisionImpairmentImage.color.g, foodVisionImpairmentImage.color.b, 0);
        visionImpairedTimer = playerDamage.VisionImpairedTimer;
        fadeTime = playerDamage.FadeTime;
        visionImpairedCounter = -1f;
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        visionImpairedCounter -= Time.deltaTime;
        if (visionImpairedCounter >= 0)
        {
            //foodVisionImpairmentImage.enabled = false;

            if (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime; // Count up
                foodVisionImpairmentImage.color = new Color(foodVisionImpairmentImage.color.r, foodVisionImpairmentImage.color.g, foodVisionImpairmentImage.color.b, Mathf.Lerp(startingAlpha, 0f, elapsedTime / fadeTime));
            }
        } else
        {
            foodVisionImpairmentImage.color = new Color(foodVisionImpairmentImage.color.r, foodVisionImpairmentImage.color.g, foodVisionImpairmentImage.color.b, 0f);

        }

        if (foodVisionImpairmentImage.color.a == 0f)
        {
            if (playerDamage.GameOver)
            {
                foodVisionImpairmentImage.enabled = false;
            }

        }
    }

    public void SplashImageOn()
    {
        elapsedTime = 0f;
        visionImpairedCounter = visionImpairedTimer;
        foodVisionImpairmentImage.color = new Color(foodVisionImpairmentImage.color.r, foodVisionImpairmentImage.color.g, foodVisionImpairmentImage.color.b, startingAlpha);
    }
}
