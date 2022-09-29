using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherTestV2 : MonoBehaviour
{
    [SerializeField]
    private GameObject pivotPoint;
    [SerializeField]
    private GameObject launchPoint;
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    [Range(2,40)]
    private float foodVelocity;
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

        trajectooryTrace.UpdateTrajectory((launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity, foodPrefab.GetComponent<Rigidbody>(), launchPoint.transform.position);
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject food = Instantiate(foodPrefab);
            food.transform.position = launchPoint.transform.position;
            Rigidbody foodRB = food.GetComponent<Rigidbody>();
            //foodRB.AddForce(Vector3.Normalize(point2.transform.position-transform.position)*10, ForceMode.Impulse);
            foodRB.velocity = (launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity;
        }
        if (VRDevice.Device.SecondaryInputDevice.GetButtonDown(VRButton.One))
        {
            GameObject food = Instantiate(foodPrefab);
            food.transform.position = launchPoint.transform.position;
            Rigidbody foodRB = food.GetComponent<Rigidbody>();
            //foodRB.AddForce(Vector3.Normalize(point2.transform.position-transform.position)*10, ForceMode.Impulse);
            foodRB.velocity = (launchPoint.transform.position - pivotPoint.transform.position).normalized * foodVelocity;
        }



    }
}
