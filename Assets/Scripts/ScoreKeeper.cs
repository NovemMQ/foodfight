using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    #region "SINGLETON"
    //Singleton
    private static ScoreKeeper instance;
    public static ScoreKeeper Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("There is no ScoreKeeper instance in the scene");
            }
            return instance;
        }
    }

     private void Awake()
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

    private int foodThrown;
    private int enemyDeath;
    private int playerGotHit; 

    void Start()
    {
        foodThrown = 0;
        enemyDeath = 0;
        playerGotHit = 0;
    }


}