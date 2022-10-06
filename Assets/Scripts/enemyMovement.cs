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
    [SerializeField] private float travelTimelimit = 10;// if takes too long to destination, reset
    private float resetDestCounter = 0;
    void Start()
    { 
        agent = GetComponent<NavMeshAgent>();
        waitCounter = pauseWaitTime;
        resetDestCounter = travelTimelimit;
    }

    private void Update()
    {
        resetDestCounter -= Time.deltaTime;
        if(resetDestCounter <= 0)
        {
            resetValues();
        }
    }

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
                resetValues();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }
    }
    private void resetValues()
    {
        moving = false; //get new destination
        waitCounter = pauseWaitTime;
        resetDestCounter = travelTimelimit;
        Destination.IsOccupied = false; //cancle waypoint occupied booking
    }
    
}