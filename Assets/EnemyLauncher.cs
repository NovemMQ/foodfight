using Liminal.SDK.VR.Avatars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLauncher : MonoBehaviour
{
    Transform player;
    [SerializeField] private Vector3 PlayerTargetOffset;
    [Range(0, 10)]
    public float shootFrequency;
    private float timer;
    private float randomisedTimer;
    [Range(0, 10)]
    public float shootFrequencyRandomRange;
    [Range(0, 30)]
    public float foodVelocity;
    [Range(0, 30)]
    public float rotatePower;
    public List<GameObject> foodPrefabs;
    private Animator animator;
    public Transform throwPoint;
    private enemyMovement enemy;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        player = FindObjectOfType<VRAvatarHead>().GetComponent<Transform>();
        timer = shootFrequency;
        randomisedTimer = Random.Range(0, shootFrequencyRandomRange);
        enemy = gameObject.GetComponent<enemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            randomisedTimer -= Time.deltaTime;
            if (enemy.EnemyIsNotMoving)
            {
                if (randomisedTimer <= 0)
                {
                    animator.SetTrigger("Throw");
                }
            }
        }
    }

    void Shoot()
    {
        timer = shootFrequency;
        randomisedTimer = Random.Range(0, shootFrequencyRandomRange);
        GameObject randomFoodRandom = foodPrefabs[(int)Random.Range(0, foodPrefabs.Count - 0.01f)];
        //GameObject food = Instantiate(randomFoodRandom);
        GameObject food = FoodPoolManager.getRandomFoodEnemy();
        food.transform.position = throwPoint.position;//transform.position + (Vector3.up * 3);
        food.SetActive(true);
        Rigidbody foodRB = food.GetComponent<Rigidbody>();

        Vector3 direction = (player.position + PlayerTargetOffset) - food.transform.position;

        //foodRB.AddForce(Vector3.Normalize(point2.transform.position-transform.position)*10, ForceMode.Impulse);
        foodRB.velocity = ((player.position + PlayerTargetOffset) - transform.position).normalized * foodVelocity;
        foodRB.AddTorque(new Vector3(Random.Range(0, rotatePower), Random.Range(0, rotatePower), Random.Range(0, rotatePower)), ForceMode.Impulse);
    }
}
