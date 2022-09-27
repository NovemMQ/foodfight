using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherTest : MonoBehaviour
{
    [SerializeField]
    private GameObject point2;
    [SerializeField]
    private GameObject foodPrefab;
    private Transform leftHandTransform;
    private Transform rightHandTransform;
    [SerializeField]
    private TrajectooryTrace trajectooryTrace;
    private void Awake()
    {
        leftHandTransform = UserInputs.Instance.LeftHandTransform;
        rightHandTransform = UserInputs.Instance.RightHandTransform;
    }

    // Update is called once per frame
    void Update()
    {

        trajectooryTrace.UpdateTrajectory(Vector3.Normalize(point2.transform.position - transform.position) * 10, foodPrefab.GetComponent<Rigidbody>(), transform.position);
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject food = Instantiate(foodPrefab);
            food.transform.position = transform.position;
            Rigidbody foodRB = food.GetComponent<Rigidbody>();
            foodRB.AddForce(Vector3.Normalize(point2.transform.position-transform.position)*10, ForceMode.Impulse);

        }
        if (leftHandTransform != null && rightHandTransform != null)
        {
            if (VRDevice.Device.SecondaryInputDevice.GetButtonDown(VRButton.One))
            {
                GameObject food = Instantiate(foodPrefab);
                food.transform.position = leftHandTransform.position;
                Rigidbody foodRB = food.GetComponent<Rigidbody>();
                foodRB.velocity = (Vector3.Normalize(rightHandTransform.position - leftHandTransform.position) * 10);
            }
        }
  

    }
}
