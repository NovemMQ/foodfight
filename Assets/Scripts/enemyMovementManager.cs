﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private int maxEnemyInScene = 3;
    private bool maxInScene = false;

    public Transform StartSpwanPoint { get => startSpwanPoint; }
    public Transform EndSpwanPoint { get => endSpwanPoint; }

    void Start()
    {
        waypointList = waypointListOb.GetComponentsInChildren<waypointScript>();
        enemyPoolList = enemyPoolListOb.GetComponentsInChildren<enemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //IsMaxEnemyInScene();
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
        maxInScene = enemyInSceneList.Length >= maxEnemyInScene;
        return maxInScene;
    }
    
    private void sendEnemiesIntoScene()
    {
        for (int i = 0; i < enemyPoolList.Length && !maxInScene; i++)
        {
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
        SetDestination(enemy, startSpwanPoint.GetComponent<waypointScript>());
        enemy.SetLauncherActive(false);//turn on launcher
        //upodate the array lists 
        IsMaxEnemyInScene(); //update and check if there are max number of enemies in scene
    }

}
