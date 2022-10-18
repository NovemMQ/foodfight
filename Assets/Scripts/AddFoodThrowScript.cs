using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFoodThrowScript : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.addFoodThrown();
        uiManager = FindObjectOfType<UIManager>();
        uiManager.SetFoodThrownUI(scoreKeeper.FoodThrown);

    }

}
