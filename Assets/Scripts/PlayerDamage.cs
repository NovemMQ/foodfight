using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private ScoreKeeper ScoreManager;
    private void Start()
    {
        ScoreManager = FindObjectOfType<ScoreKeeper>();
    }
    //die when hit by food
    private void OnTriggerEnter(Collider other)
    {
        if (TagManager.CompareTags(other.gameObject, "enemyFood"))
        {
            ScoreManager.addPlayerGotHit();
        }
    }
}
