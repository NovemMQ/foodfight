using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementManager : MonoBehaviour
{

    public GameObject enemyListOb;
    public enemyMovement[] enemyList;
    public waypointScript[] waypointList;

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
        Vector3 nextdestination = enemy.transform.position;
        wp.IsOccupied = true;
        wp.OccupiedBy = enemy;
        nextdestination = wp.transform.position;
        enemy.Destination = wp;
        enemy.moving = true;
        enemy.agent.destination = nextdestination;
        Debug.Log("EM is " + wp.gameObject.name);
        
    }

}
