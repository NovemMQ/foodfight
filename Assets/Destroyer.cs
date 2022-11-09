using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject[] splatDecals;
    GameObject splatDecal;
    [SerializeField]
    private float deathTimer = 3f;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = deathTimer;
        splatDecal = splatDecals[Random.Range(0, splatDecals.Length)];
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
        splat.transform.LookAt(collision.contacts[0].normal, Vector3.up);
        splat.transform.localScale = splat.transform.localScale * Random.Range(0.5f, 0.9f);
        Destroy(this.gameObject);
    }
}
