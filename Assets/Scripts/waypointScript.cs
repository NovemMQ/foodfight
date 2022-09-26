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
        Debug.Log("Here----- waypoints 2" + other.gameObject.name +" and is the same as "+ occupiedBy.gameObject.name);
        if (other.gameObject.name.Equals(occupiedBy.gameObject.name))
        {

            isOccupied = false;
        }
    }
}
