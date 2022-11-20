using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerV2 : MonoBehaviour
{
    [SerializeField]
    private float deathTimer = 3f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = deathTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveAndEnabled)
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            FoodPoolManager.AddItemEnemy(this.gameObject);

        }
    }
    private void OnDisable()
    {
        timer = deathTimer;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagObject>())
        {
            if (!TagManager.CompareTags(other.gameObject, "enemyFood"))
            {
                Debug.Log("penis");
                //       GameObject spFX = Instantiate(sparklePFX);
                //       spFX.transform.position = transform.position;
                //       ParticleSystem spFXPFX = spFX.GetComponent<ParticleSystem>();
                //       spFXPFX.Play();

                FoodPoolManager.AddItemEnemy(this.gameObject);
            }
        }
    }
 }
