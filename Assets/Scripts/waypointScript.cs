using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointScript : MonoBehaviour
{
    private bool isOccupied = false;
    private enemyMovement occupiedBy;

    public enemyMovement OccupiedBy {set { occupiedBy = value; }}
    public bool IsOccupied { get { return isOccupied; } set{ isOccupied = value; } }

    void OnTriggerExit(Collider other)
    {
        if (occupiedBy) {
            if (other.name.Equals(occupiedBy.name))
            {
                isOccupied = false;
            }
        }
    }
}
