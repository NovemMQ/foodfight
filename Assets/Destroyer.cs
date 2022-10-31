using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]
    GameObject splatDecal;
    [SerializeField]
    GameObject sparklePFX;
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
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject splat = Instantiate(splatDecal);
        splat.transform.position = this.transform.position;
        Destroy(this.gameObject);

        if (collision.gameObject.GetComponent<TagObject>())
        {
            if (!TagManager.CompareTags(collision.gameObject, "playerFood"))
            {
                Debug.Log("penis");
                GameObject spFX = Instantiate(sparklePFX);
                spFX.transform.position = transform.position;
                ParticleSystem spFXPFX = spFX.GetComponent<ParticleSystem>();
                spFXPFX.Play();
                Destroy(this.gameObject);
            }
        }
    }
  /*  private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TagObject>())
        {
            if (!TagManager.CompareTags(other.gameObject, "playerFood"))
            {
                Debug.Log("penis");
                GameObject spFX = Instantiate(sparklePFX);
                spFX.transform.position = transform.position;
                ParticleSystem spFXPFX = spFX.GetComponent<ParticleSystem>();
                spFXPFX.Play();
                Destroy(this.gameObject);
            }
        }
    }*/
}
