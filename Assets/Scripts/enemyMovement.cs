// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class enemyMovement : MonoBehaviour
{
    //Singletons
    //enemy movement manager script
    private enemyMovementManager EnemyMovementManager;
    //score keeper script to add points when enemy is hit
    private ScoreKeeper ScoreManager;

    //enemy model and nav mesh agent
    public ThirdPersonCharacter charactor;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { set { agent = value; } get { return agent; } }
    
    //destination 
    private waypointScript destination;  
    public waypointScript Destination { set { destination = value; } get { return destination; } }

    //enemy boolean, check moving booleans
    public bool moving = false; //this boolean if false the enemy will set a new destination 
    private bool enemyIsNotMoving;
    public bool EnemyIsNotMoving { get => enemyIsNotMoving; set => enemyIsNotMoving = value; } // when at destination = true!

    //enemy timers 
    [SerializeField] private float pauseWaitTime = 3; // secs, when at destination stand there for this amount of time
    private float waitCounter = 0; //secs
    [SerializeField] private float travelTimelimit = 10;// if takes too long to destination, reset the destination
    private float resetDestCounter = 0;

    [SerializeField] private float inSceneTimelimit = 20f; // after this timelimit the enemy goes to end spwan point and despawns/leaves the scene
    private float inSceneCounter = 0f;
    [SerializeField] private float inSceneMinLimit = 8f;

    //launcher script 
    [SerializeField] private EnemyLauncher enemyLauncher;
    private Animator animator;

    void Start()
    {
        //get singletons 
        EnemyMovementManager = FindObjectOfType<enemyMovementManager>();
        ScoreManager = FindObjectOfType<ScoreKeeper>();
        //get nav mesh agent AI, set wait time, and counter
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        waitCounter = pauseWaitTime;
        resetDestCounter = travelTimelimit;
        inSceneCounter = inSceneTimelimit;
        if (enemyLauncher)
        {
            enemyLauncher.enabled = false;//turn off launcher
        }
        animator = GetComponent<Animator>();//enemy animations 
        enemyIsNotMoving = false;
    }

    private void Update()
    {
        ManageEnemyInSceneTimeLimit();
        ResetDestinationIfEnemyTravlingTooLong();
        AnimationNavMeshAgentManager();
    }

    private void ManageEnemyInSceneTimeLimit()
    {
        inSceneCounter -= Time.deltaTime; //the Max time for the enemy to be in scene
        if (inSceneCounter <= 0)
        {
            ResetInSceneCounter(); //incase enemy was trapped try again.
            resetDestCounter = travelTimelimit; //reset timer for trival limit, incase enemy was trapped, go to a waypoint instead
            EnemyMovementManager.SendEnemyToEndSpwanPoint(this);
        }
    }

    private void ResetDestinationIfEnemyTravlingTooLong()
    {
        //if enemy is still travling to destination after timelimit, choose a new destination
        resetDestCounter -= Time.deltaTime;
        if (resetDestCounter <= 0)
        {
            ResetValues();
        }
    }

    private void AnimationNavMeshAgentManager()
    {
        // Unity NavMesh Tutorial - Animated Character https://www.youtube.com/watch?v=blPglabGueM by Brackeys
        if (agent)
        {
            if (agent.isActiveAndEnabled)
            {
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    charactor.Move(agent.desiredVelocity, false, true);
                    enemyIsNotMoving = false;
                }
                else
                {
                    charactor.Move(Vector3.zero, false, false);
                    enemyIsNotMoving = true;
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //when enemy is at destination, start idle/wait counter
        if (destination) //if dest is not null
        {
            if (collision.gameObject.name.Equals(destination.name))
            {
                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0f)
                {
                    ResetValues();
                }
            }
        }
    }

    //die when hit by food
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<TagObject>())
        {
            if (TagManager.CompareTags(collision.gameObject, "playerFood"))
            {
                animator.SetTrigger("Damage");
            }
        }
    }

    //It's used within the Damage animation, see the enmy animation controller where this is being called
    private void Die()
    {
        EnemyMovementManager.SendEnemyToStartSpwanPoint(this); 
        ScoreManager.addEnemyDeath();
    }

    private void ResetValues()
    {
        moving = false; //get new destination
        waitCounter = pauseWaitTime;
        resetDestCounter = travelTimelimit;
        if (Destination != null)
        {
            Destination.IsOccupied = false; //cancle waypoint occupied booking
        } 
    }
    
    //turn off the enemy throwing food
    public void SetLauncherActive(bool active)
    {
        enemyLauncher.enabled = active;
    }

    public void ResetInSceneCounter()
    {
        inSceneCounter = Random.Range(inSceneMinLimit, inSceneTimelimit);
    }

    //stop moving by changing the enemy destination to its current spot, does not save the previous destination
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

    //start moving by reseting the values thus enemy will be set a new waypoint destination
    public void ResumeMoving()
    {
        ResetValues();//set new destinations
    }
}