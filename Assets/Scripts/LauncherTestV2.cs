using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherTestV2 : MonoBehaviour
{
    [SerializeField] private GameManager gameMnager;
   
    [SerializeField]
    private GameObject pivotPoint;
    [SerializeField]
    private GameObject launchPoint;
    public List<GameObject> foodPrefab;
    [SerializeField]
    [Range(2,40)]
    private float foodVelocity;
    [SerializeField]
    [Range(2, 20)]
    private float rotatePower;
    private Transform leftHandTransform;
    private Transform rightHandTransform;
    [SerializeField]
    private TrajectooryTrace trajectooryTrace;
    private void Awake()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        trajectooryTrace.UpdateTrajectory((launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity, foodPrefab[0].GetComponent<Rigidbody>(), launchPoint.transform.position);
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject randomFoodRandom = foodPrefab[(int)Random.Range(0, foodPrefab.Count - 0.01f)];
            GameObject food = Instantiate(randomFoodRandom);
            food.transform.position = launchPoint.transform.position;
            Rigidbody foodRB = food.GetComponent<Rigidbody>();
            //foodRB.AddForce(Vector3.Normalize(point2.transform.position-transform.position)*10, ForceMode.Impulse);
            foodRB.velocity = (launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity;
        }
        if (VRDevice.Device.SecondaryInputDevice.GetButtonDown(VRButton.One))
        {
            GameObject randomFoodRandom = foodPrefab[(int)Random.Range(0, foodPrefab.Count - 0.01f)];
            GameObject food = Instantiate(randomFoodRandom);
            food.transform.position = launchPoint.transform.position;
            Rigidbody foodRB = food.GetComponent<Rigidbody>();
            //foodRB.AddForce(Vector3.Normalize(point2.transform.position-transform.position)*10, ForceMode.Impulse);
            foodRB.velocity = (launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity;
            foodRB.AddTorque(new Vector3(Random.Range(0, rotatePower), Random.Range(0, rotatePower), Random.Range(0, rotatePower)), ForceMode.Impulse);
            //foodRB.AddTorque(Vector3.left*rotatePower, ForceMode.Impulse);
        }



    }
}
