using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovementManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyPoolListOb; //enemy object pool
    [SerializeField] private GameObject enemyInSceneListOb; //enemy in the scene list
    [SerializeField] private GameObject waypointListOb;// list of enemy waypoint objects
    [SerializeField] private enemyMovement[] enemyPoolList;
    [SerializeField] private enemyMovement[] enemyInSceneList;
    [SerializeField] private waypointScript[] waypointList;
    [SerializeField] private Transform startSpwanPoint;
    [SerializeField] private Transform endSpwanPoint;
    [Range(0,60)]
    [SerializeField] private float spawnRateChangeTime = 10;//sec, random number of enemy in the scene every #secs
    private float spawnRateChangeCounter;
    [Range(0, 10)]
    [SerializeField] private int minEnemyInScene = 5;
    [Range(0, 19)]
    [SerializeField] private int maxEnemyInScene = 10;
    private int numEnemyInScene;
    private bool maxInScene = false;

    public Transform StartSpwanPoint { get => startSpwanPoint; }
    public Transform EndSpwanPoint { get => endSpwanPoint; }

    void Start()
    {
        spawnRateChangeCounter = 100000000;//set to unreasonable big number, will be reset to differnt number in random enemy wave
        waypointList = waypointListOb.GetComponentsInChildren<waypointScript>();
        enemyPoolList = enemyPoolListOb.GetComponentsInChildren<enemyMovement>();
        numEnemyInScene = minEnemyInScene;
    }

    // Update is called once per frame
    void Update()
    {
        spawnRateChangeCounter -= Time.deltaTime;
        if (spawnRateChangeCounter <= 0) //randomly pick enemy wave numbers in scene after a time period
        {
            RandomEnemyWaves();
        }
        SendEnemiesIntoScene();
        SetAllEnemyMovementInList(enemyInSceneList);
    }

    private void RandomEnemyWaves()
    {
        numEnemyInScene = (int)Random.Range(minEnemyInScene, (maxEnemyInScene + 1));
        IsMaxEnemyInScene();
        spawnRateChangeCounter += spawnRateChangeTime;
    }

    //set an enemy waypoints
    private void SetDestination(enemyMovement enemy, waypointScript wp)
    {
        wp.IsOccupied = true;//tell the waypoint that is has a booking
        wp.OccupiedBy = enemy;//booking by
        enemy.Destination = wp;//tell the enemy what destination was booked for them
        enemy.moving = true;//make sure they are moving so no new destination is set for them when true
        if (enemy.Agent.isActiveAndEnabled) //turn of nav mesh agent
        {
            enemy.Agent.destination = wp.transform.position;
        }        
    }

    //set the enemies list waypoints if they are not moving
    private void SetAllEnemyMovementInList (enemyMovement[] enemyList)
    {
        foreach (enemyMovement enemy in enemyList)
        {
            if (enemy.moving == false)
            {
                waypointScript wp = waypointList[Random.Range(0, waypointList.Length)];

                if (wp.IsOccupied == false)
                {
                    SetDestination(enemy, wp);
                }
            }
        }
    }

    //check if the scene have the max amount of enemies and set the max boolean 
    private bool IsMaxEnemyInScene()
    {
        enemyPoolList = enemyPoolListOb.GetComponentsInChildren<enemyMovement>();
        enemyInSceneList = enemyInSceneListOb.GetComponentsInChildren<enemyMovement>();
        maxInScene = enemyInSceneList.Length >= numEnemyInScene;
        return maxInScene;
    }
    
    private void SendEnemiesIntoScene()
    {
        for (int i = 0; i < enemyPoolList.Length && !maxInScene; i++)
        {
            enemyPoolList[i].GetComponent<NavMeshAgent>().enabled = true;
            enemyPoolList[i].GetComponent<Animator>().enabled = true;
            enemyPoolList[i].moving = false;
            enemyPoolList[i].ResetInSceneCounter();// reset the enemy scene time limit
            enemyPoolList[i].SetLauncherActive(true);//turn on launcher
            enemyPoolList[i].gameObject.transform.SetParent(enemyInSceneListOb.transform);//move enemy into scene
            //upodate the array lists 
            IsMaxEnemyInScene(); //update and check if there are max number of enemies in scene
           
        }
    }

    public void SendEnemyToStartSpwanPoint(enemyMovement enemy)
    {
        enemy.gameObject.transform.SetParent(enemyPoolListOb.transform);//move enemy into scene
        enemy.transform.position = startSpwanPoint.position;
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        enemy.GetComponent<Animator>().enabled = false;
        SetDestination(enemy, startSpwanPoint.GetComponent<waypointScript>());
        enemy.SetLauncherActive(false);//turn on launcher
        enemy.ResetInSceneCounter();
        //upodate the array lists 
        IsMaxEnemyInScene(); //update and check if there are max number of enemies in scene
    }

    public void SendEnemyToEndSpwanPoint(enemyMovement enemy)
    {
        SetDestination(enemy, endSpwanPoint.GetComponent<waypointScript>());
        enemy.SetLauncherActive(false);//turn off launcher
    }

    public void StopEnemyWavesMovement()
    {
        numEnemyInScene = 0;
        spawnRateChangeCounter = 1000000;
        maxInScene = true;
    }

    public void StartEnemyWavesMovement()
    {
        RandomEnemyWaves();
    }
}
