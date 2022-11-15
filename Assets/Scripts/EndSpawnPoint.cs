using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSpawnPoint : MonoBehaviour
{
    private enemyMovementManager EnemyMovementManager;
    void Start()
    {
        EnemyMovementManager = FindObjectOfType<enemyMovementManager>();  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<enemyMovement>())
        {

            EnemyMovementManager.SendEnemyToStartSpwanPoint(other.GetComponent<enemyMovement>());//despawns the enemy to the start spwanpoint
        }
    }
 }
