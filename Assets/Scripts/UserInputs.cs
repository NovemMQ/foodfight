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

    public static UserInputs Instance { get => instance;}
    public Transform RightHandTransform { get => rightHandTransform;}
    public Transform LeftHandTransform { get => leftHandTransform;}

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
        IVRDevice device = VRDevice.Device;    
    }

      

    //VR controller inputs here

}