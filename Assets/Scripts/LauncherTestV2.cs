using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherTestV2 : MonoBehaviour
{
    private enum GunSide
    {
        primary,
        secondary
    }

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
    [SerializeField]
    private TrajectooryTrace trajectooryTrace;
    private float timer = 0f;
    [SerializeField]
    private GunSide gunSide;
    private void Awake()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        trajectooryTrace.UpdateTrajectory((launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity, foodPrefab[0].GetComponent<Rigidbody>(), launchPoint.transform.position);
        if (timer <= 0)
        {

            if (gunSide.Equals(GunSide.primary))
            {
                if (VRDevice.Device.PrimaryInputDevice.GetButton(VRButton.Trigger))
                {
                    Shoot();
                }
            }
            else if(gunSide.Equals(GunSide.secondary))
            {
                if (VRDevice.Device.SecondaryInputDevice.GetButton(VRButton.Trigger))
                {
                    Shoot();
                }
            }
        }
    }
    public void Shoot()
    {
        timer = 0.2f;
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
