using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class UserInputs : MonoBehaviour
{
    private static UserInputs instance;

    [SerializeField]
    private Transform rightHandTransform;
    [SerializeField]
    private Transform leftHandTransform;
    private IVRDevice device;
    private IVRInputDevice leftHand;
    private IVRInputDevice rightHand;

    public static UserInputs Instance { get => instance;}
    public Transform RightHandTransform { get => rightHandTransform;}
    public Transform LeftHandTransform { get => leftHandTransform;}
    public IVRDevice Device { get => device; set => device = value; }
    public IVRInputDevice LeftHand { get => leftHand;}
    public IVRInputDevice RightHand { get => rightHand;}

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        device = VRDevice.Device;
        leftHand = device.SecondaryInputDevice;
        rightHand = device.PrimaryInputDevice;
    }

      

    //VR controller inputs here

}