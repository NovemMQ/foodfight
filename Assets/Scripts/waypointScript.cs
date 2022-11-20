using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointScript : MonoBehaviour
{
    //boolean that check if the current waypoint destination has been boked/taken/is the destination of an enemy (meaning if an enemy has set this as a destination
    private bool isOccupied = false;
    private enemyMovement occupiedBy; // the enemy that has set this waypoint as the destination 

    public enemyMovement OccupiedBy {set { occupiedBy = value; }}
    public bool IsOccupied { get { return isOccupied; } set{ isOccupied = value; } }
    [SerializeField] private float occcupiedTooLongTimer = 40; //secs, 
    private float occcupiedTooLongCounter;

    private void Start()
    {
        occcupiedTooLongCounter = occcupiedTooLongTimer;
    }

    private void Update()
    {
        if (isOccupied)
        {
            occcupiedTooLongCounter -= Time.deltaTime;
        }

        if(occcupiedTooLongCounter <= 0 && isOccupied) //if after this timer and still no enemy has arrived then set isOccupied as false and wait for a new enemy to pick this as its destination 
        {
            isOccupied = false;
        }
    }

    void OnTriggerExit(Collider other) //check if the enemy is the same as 'occupiedBy' the enemy that booked(set this point as their destination) if so than set isOccupied to false and wait for the next enemy
    {
        if (occupiedBy) {
            if (other.name.Equals(occupiedBy.name))
            {
                occcupiedTooLongCounter = occcupiedTooLongTimer;
                isOccupied = false;
            }
        }
    }
}
