using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPoolManager : MonoBehaviour
{
    public static FoodPoolManager Instance { get; private set; }


    [SerializeField]
    List<GameObject> foodPrefabsPlayer;
    [SerializeField]
    List<GameObject> foodPrefabsEnemy;
    public static List<GameObject> generatedFoodEnemy = new List<GameObject>();
    public static List<GameObject> generatedFoodPlayer = new List<GameObject>();
    [SerializeField]
    private float maxNumberOfFood;
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance!=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        for (int i = 0; i < maxNumberOfFood; i++)
        {
            GameObject food = Instantiate(foodPrefabsEnemy[Mathf.FloorToInt(i * foodPrefabsEnemy.Count / maxNumberOfFood)]);
            food.transform.position = this.transform.position;
            generatedFoodEnemy.Add(food);
            generatedFoodEnemy[i].SetActive(false);
        }
        for (int i = 0; i < maxNumberOfFood; i++)
        {
            GameObject food = Instantiate(foodPrefabsPlayer[Mathf.FloorToInt(i * foodPrefabsPlayer.Count / maxNumberOfFood)]);
            food.transform.position = this.transform.position;
            generatedFoodPlayer.Add(food);
            generatedFoodPlayer[i].SetActive(false);
        }
    }
    public static GameObject getRandomFoodEnemy()
    {
        int number = Random.Range(0, generatedFoodEnemy.Count);
        GameObject food = generatedFoodEnemy[number];
        generatedFoodEnemy.RemoveAt(number);
        return food;
    }
    public static void AddItemEnemy(GameObject food)
    {
        food.SetActive(false);
        generatedFoodEnemy.Add(food);
    }
    public static GameObject getRandomFoodPlayer()
    {
        int number = Random.Range(0, generatedFoodPlayer.Count);
        GameObject food = generatedFoodPlayer[number];
        generatedFoodEnemy.RemoveAt(number);
        return food;
    }
    public static void AddItemPlayer(GameObject food)
    {
        food.SetActive(false);
        generatedFoodPlayer.Add(food);
    }
}
