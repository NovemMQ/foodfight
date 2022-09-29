using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerEnemy : MonoBehaviour
{
    Destroyer typeCheck;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("yeet");
        typeCheck = other.gameObject.GetComponent<Destroyer>();
        if (typeCheck != null)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("yeet");
        typeCheck = collision.gameObject.GetComponent<Destroyer>();
        if (typeCheck != null)
        {
            Destroy(this.gameObject);
        }
    }
}
