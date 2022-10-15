// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class enemyMovement : MonoBehaviour
{
    public ThirdPersonCharacter charactor;
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

    //launcher script 
    [SerializeField] private EnemyLauncher enemyLauncher;

    void Start()
    { 
        //get nav mesh agent AI, set wait time, and counter
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        waitCounter = pauseWaitTime;
        resetDestCounter = travelTimelimit;
        enemyLauncher.enabled = false;//turn off launcher
    }

    private void Update()
    {
        //if enemy is still travling to destination after timelimit, choose a new destination
        resetDestCounter -= Time.deltaTime;
        if(resetDestCounter <= 0)
        {
            resetValues();
        }

        // Unity NavMesh Tutorial - Animated Character https://www.youtube.com/watch?v=blPglabGueM by Brackeys

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            charactor.Move(agent.desiredVelocity, false, true);
        }
        else
        {
            charactor.Move(Vector3.zero, false, false);
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("dest name is: " + destination.name + " time is "+ waitCounter);
        //when enemy is at destination, start idle/wait counter
        if (destination)
        {
            if (other.name.Equals(destination.name))
            {
                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0f)
                {
                    resetValues();
                }
            }
        }
    }
  
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enemy death: trigger");
        if(TagManager.CompareTags(other.gameObject, "playerFood"))
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
    
    public void SetLauncherActive(bool active)
    {
        enemyLauncher.enabled = active;
    }
}