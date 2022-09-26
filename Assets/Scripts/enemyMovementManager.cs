using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovementManager : MonoBehaviour
{

    public GameObject enemyListOb;
    public enemyMovement[] enemyList;
    public Transform[] waypointList;
    private int current = 0;

    void Start()
    {
        waypointList = gameObject.GetComponentsInChildren<Transform>();
        enemyList = enemyListOb.GetComponentsInChildren<enemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (enemyMovement enemy in enemyList) {
            if (enemy.moving == false)
            {
                current++;
                if (current >= waypointList.Length)
                {
                    current = 1;
                }
                enemy.agent.destination = waypointList[current].position;
                enemy.moving = true;
            } }
    }

}
