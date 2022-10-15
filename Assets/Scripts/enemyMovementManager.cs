using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyListOb;
    [SerializeField] private enemyMovement[] enemyList;
    [SerializeField] private waypointScript[] waypointList;

    void Start()
    {
        waypointList = gameObject.GetComponentsInChildren<waypointScript>();
        enemyList = enemyListOb.GetComponentsInChildren<enemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (enemyMovement enemy in enemyList) {
            if (enemy.moving == false)
            {
                waypointScript wp = waypointList[Random.Range(0, waypointList.Length)];
  
                if (wp.IsOccupied == false)
                {
                    setDestination(enemy, wp);
                }
            }
        }
    }

    private void setDestination(enemyMovement enemy, waypointScript wp)
    {
        wp.IsOccupied = true;
        wp.OccupiedBy = enemy;
        enemy.Destination = wp;
        enemy.moving = true;
        enemy.Agent.destination = wp.transform.position;
        //Debug.Log("EM is " + wp.gameObject.name);
        
    }

}
