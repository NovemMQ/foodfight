// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{

    //public Transform goal;
    public bool moving = false;
    private NavMeshAgent agent;
    private waypointScript destination;  
    public waypointScript Destination { set { destination = value; } get { return destination; } }
    public NavMeshAgent Agent { set { agent = value; } get { return agent; } }
    [SerializeField] private float pauseWaitTime = 3; // secs
    private float waitCounter = 0; //secs
    void Start()
    { 
        agent = GetComponent<NavMeshAgent>();
        waitCounter = pauseWaitTime;
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(destination.name) && waitCounter <= 0f)
        {
            moving = false;
            waitCounter += pauseWaitTime;
        }
    }*/
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("dest name is: " + destination.name + " time is "+ waitCounter);
        if (destination)
        {
            if (other.name.Equals(destination.name))
            {
                waitCounter -= Time.deltaTime;
            }
            if (other.name.Equals(destination.name) && waitCounter <= 0f)
            {
                moving = false;
                waitCounter += pauseWaitTime;
            }
        }
    }
    
}