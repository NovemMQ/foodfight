using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class UserInputs : MonoBehaviour
{
    private static UserInputs instance;
    [SerializeField]
    private VRAvatarHand rightHandAvatarHand;
    [SerializeField]
    private VRAvatarHand leftHandAvatarHand;
    private Transform rightHandTransform;
    private Transform leftHandTransform;
    private IVRDevice device;
    private IVRInputDevice leftHand;
    private IVRInputDevice rightHand;

    public static UserInputs Instance { get => instance; }
    public Transform RightHandTransform { get => rightHandTransform; }
    public Transform LeftHandTransform { get => leftHandTransform; }
    public IVRDevice Device { get => device; set => device = value; }
    public IVRInputDevice LeftHand { get => leftHand; }
    public IVRInputDevice RightHand { get => rightHand; }
    public VRAvatarHand RightHandAvatarHand { get => rightHandAvatarHand;}
    public VRAvatarHand LeftHandAvatarHand { get => leftHandAvatarHand;}

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
        leftHandTransform = leftHandAvatarHand.transform;
        rightHandTransform = rightHandAvatarHand.transform;
        //       leftHand = device.SecondaryInputDevice;
        //       rightHand = device.PrimaryInputDevice;
    }
    
    private void Update()
    {
        Debug.Log(leftHandAvatarHand.Transform.position);
        
    }

    //VR controller inputs here

}