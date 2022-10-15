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
    [SerializeField] private float spawnRateChangeTime = 30;//sec, random number of enemy in the scene every #secs
    [SerializeField] private float spawnRateChangeCounter;
    [SerializeField] private int minEnemyInScene = 3;
    [SerializeField] private int maxEnemyInScene = 10;
    [SerializeField] private int numEnemyInScene;
    private bool maxInScene = false;

    public Transform StartSpwanPoint { get => startSpwanPoint; }
    public Transform EndSpwanPoint { get => endSpwanPoint; }

    void Start()
    {
        spawnRateChangeCounter = spawnRateChangeTime;
        waypointList = waypointListOb.GetComponentsInChildren<waypointScript>();
        enemyPoolList = enemyPoolListOb.GetComponentsInChildren<enemyMovement>();
        numEnemyInScene = minEnemyInScene;
    }

    // Update is called once per frame
    void Update()
    {
        spawnRateChangeCounter -= Time.deltaTime;
        if (spawnRateChangeCounter <= 0)
        {
            spawnRateChangeCounter += spawnRateChangeTime;
            numEnemyInScene = Random.Range(minEnemyInScene, maxEnemyInScene+1);
            IsMaxEnemyInScene();
        }
        sendEnemiesIntoScene();
        SetAllEnemyMovementInList(enemyInSceneList);
    }

    //set an enemy waypoints
    private void SetDestination(enemyMovement enemy, waypointScript wp)
    {
        wp.IsOccupied = true;
        wp.OccupiedBy = enemy;
        enemy.Destination = wp;
        enemy.moving = true;
        enemy.Agent.destination = wp.transform.position;
        //Debug.Log("EM is " + wp.gameObject.name);
        
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
    
    private void sendEnemiesIntoScene()
    {
        for (int i = 0; i < enemyPoolList.Length && !maxInScene; i++)
        {
            enemyPoolList[i].GetComponent<NavMeshAgent>().enabled = true;
            enemyPoolList[i].moving = false;
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
        SetDestination(enemy, startSpwanPoint.GetComponent<waypointScript>());
        enemy.SetLauncherActive(false);//turn on launcher
        enemy.resetInSceneCounter();
        //upodate the array lists 
        IsMaxEnemyInScene(); //update and check if there are max number of enemies in scene
    }

    public void SendEnemyToEndSpwanPoint(enemyMovement enemy)

    {
        SetDestination(enemy, endSpwanPoint.GetComponent<waypointScript>());
        enemy.SetLauncherActive(false);//turn off launcher
    }

}
