// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{

    //public Transform goal;
    public bool moving = false;
    public NavMeshAgent agent;

    void Start()
    { 
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
    }

    void Update()
    {
        if((gameObject.transform.position.x == agent.destination.x) && (gameObject.transform.position.z == agent.destination.z))
        {
            moving = false;
        }
    }
}