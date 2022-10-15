using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyPoolListOb; //enemy object pool
    [SerializeField] private GameObject enemyInSceneListOb; //enemy in the scene list
    [SerializeField] private enemyMovement[] enemyPoolList;
    [SerializeField] private enemyMovement[] enemyInSceneList;
    [SerializeField] private waypointScript[] waypointList;
    [SerializeField] private int maxEnemyInScene = 3;
    private bool maxInScene = false;

    void Start()
    {
        waypointList = gameObject.GetComponentsInChildren<waypointScript>();
        enemyPoolList = enemyPoolListOb.GetComponentsInChildren<enemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        IsMaxEnemyInScene();
        for (int i = 0; i < enemyPoolList.Length && !maxInScene; i++)
        {
            enemyPoolList[i].SetLauncherActive(true);//turn on launcher
            enemyPoolList[i].gameObject.transform.SetParent(enemyInSceneListOb.transform);
            enemyPoolList = enemyPoolListOb.GetComponentsInChildren<enemyMovement>();
            enemyInSceneList = enemyInSceneListOb.GetComponentsInChildren<enemyMovement>();
            IsMaxEnemyInScene();
        }
        SetAllEnemyMovementInList(enemyInSceneList);
    }

    private void SetDestination(enemyMovement enemy, waypointScript wp)
    {
        wp.IsOccupied = true;
        wp.OccupiedBy = enemy;
        enemy.Destination = wp;
        enemy.moving = true;
        enemy.Agent.destination = wp.transform.position;
        //Debug.Log("EM is " + wp.gameObject.name);
        
    }

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

    private bool IsMaxEnemyInScene()
    {
        maxInScene = enemyInSceneList.Length >= maxEnemyInScene;
        return maxInScene;
    }

}
