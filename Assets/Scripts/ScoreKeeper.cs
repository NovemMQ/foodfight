using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    #region SINGLETON
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

    [SerializeField] private int foodThrown;
    public int FoodThrown{ get { return foodThrown; }}
    [SerializeField] private int enemyDeath;
    public int EnemyDeath { get { return enemyDeath; }}
    [SerializeField] private int playerGotHit;
    public int PlayerGotHit { get { return playerGotHit; }}

    void Start()
    {
        foodThrown = 0;
        enemyDeath = 0;
        playerGotHit = 0;
    }

    //increment foodThrown
    public void addFoodThrown()
    {
        foodThrown += 1;
    }

    //increment enemyDeath
    public void addEnemyDeath()
    {
        enemyDeath += 1;
    }

    //increment playerGotHit
    public void addPlayerGotHit()
    {
        playerGotHit += 1;
    }
}