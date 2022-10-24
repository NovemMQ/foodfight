// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class enemyMovement : MonoBehaviour
{
    private enemyMovementManager EnemyMovementManager;
    private ScoreKeeper ScoreManager;
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

    [SerializeField] private float inSceneTimelimit = 15; // after this timelimit the enemy goes to end spwan point and despawns/leaves the scene
    private float inSceneCounter = 0;

    //launcher script 
    [SerializeField] private EnemyLauncher enemyLauncher;
    private Animator animator;

    void Start()
    {
        EnemyMovementManager = FindObjectOfType<enemyMovementManager>();
        ScoreManager = FindObjectOfType<ScoreKeeper>();
        //get nav mesh agent AI, set wait time, and counter
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        waitCounter = pauseWaitTime;
        resetDestCounter = travelTimelimit;
        inSceneCounter = inSceneTimelimit;
        enemyLauncher.enabled = false;//turn off launcher
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inSceneCounter -= Time.deltaTime; //the Max time for the enemy to be in scene
        if (inSceneCounter <= 0)
        {
            resetInSceneCounter(); //incase enemy was trapped try again.
            resetDestCounter = travelTimelimit; //reset timer for trival limit, incase enemy was trapped, go to a waypoint instead
            EnemyMovementManager.SendEnemyToEndSpwanPoint(this);
        }
        //if enemy is still travling to destination after timelimit, choose a new destination
        resetDestCounter -= Time.deltaTime;
        if(resetDestCounter <= 0)
        {
            resetValues();
        }

        // Unity NavMesh Tutorial - Animated Character https://www.youtube.com/watch?v=blPglabGueM by Brackeys
        if (agent.isActiveAndEnabled)
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                charactor.Move(agent.desiredVelocity, false, true);
            }
            else
            {
                charactor.Move(Vector3.zero, false, false);
            }
        }

        //test stop and resume
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopMoving();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ResumeMoving();
        }
        */
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
  
    //die when hit by food
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TagObject>())
        {
            if (TagManager.CompareTags(other.gameObject, "playerFood"))
            {
                //Destroy(this.gameObject);
                // EnemyMovementManager.SendEnemyToStartSpwanPoint(this);
                // ScoreManager.addEnemyDeath();
                animator.SetTrigger("Damage");
            }
        }
    }

    private void Die()
    {
        EnemyMovementManager.SendEnemyToStartSpwanPoint(this);
        ScoreManager.addEnemyDeath();
    }

    private void resetValues()
    {
        moving = false; //get new destination
        waitCounter = pauseWaitTime;
        resetDestCounter = travelTimelimit;
        if (Destination != null)
        {
            Destination.IsOccupied = false; //cancle waypoint occupied booking
        } 
    }
    
    public void SetLauncherActive(bool active)
    {
        enemyLauncher.enabled = active;
    }

    public void resetInSceneCounter()
    {
        inSceneCounter = inSceneTimelimit;
    }

    public void StopMoving()
    {
        moving = true;//don't get new destination
        resetDestCounter = travelTimelimit;
        waitCounter = pauseWaitTime;
        Destination.IsOccupied = false; //cancle waypoint occupied booking
        Destination = null;
        if (Agent.isActiveAndEnabled == true)
        {
            agent.destination = transform.position;
        }
    }

    public void ResumeMoving()
    {
        resetValues();//set new destinations
    }
}