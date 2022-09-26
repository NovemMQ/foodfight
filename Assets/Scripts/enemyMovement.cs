// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{

    //public Transform goal;
    public bool moving = false;
    public NavMeshAgent agent;
    private waypointScript destination;  
    public waypointScript Destination { set { destination = value; } get { return destination; } }

    void Start()
    { 
        agent = GetComponent<NavMeshAgent>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Here------------2 " + other.gameObject.name);
        if (other.gameObject.name.Equals(destination.gameObject.name))
        {
            moving = false;
        }
    }

}