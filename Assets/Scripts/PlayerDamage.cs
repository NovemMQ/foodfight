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
    float startingAlpha; //this gets the current volume of the audio listener so that we can fade it to 0 over time.

    private void Start()
    {
        ScoreManager = FindObjectOfType<ScoreKeeper>();
        startingAlpha = foodVisionImpairmentImage.color.a;
        foodVisionImpairmentImage.color = new Color(foodVisionImpairmentImage.color.r, foodVisionImpairmentImage.color.g, foodVisionImpairmentImage.color.b, 100);
        elapsedTime = 0f;
    }

    private void Update()
    {
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
        }
    }
}
