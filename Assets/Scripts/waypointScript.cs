using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointScript : MonoBehaviour
{
    private bool isOccupied = false;
    private enemyMovement occupiedBy;

    public enemyMovement OccupiedBy {set { occupiedBy = value; }}
    public bool IsOccupied { get { return isOccupied; } set{ isOccupied = value; } }
    [SerializeField] private float occcupiedTooLongTimer = 40; //secs
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

        if(occcupiedTooLongCounter <= 0 && isOccupied)
        {
            isOccupied = false;
        }
    }

    void OnTriggerExit(Collider other)
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
