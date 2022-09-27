using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using UnityEngine;

public class UserInputs : MonoBehaviour
{
    private static UserInputs instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }


    [SerializeField]
    private VRAvatarHand rightHand;
    [SerializeField]
    private VRAvatarHand leftHand;
    public VRAvatarHand LeftHand { get => leftHand;/* set => leftHand = value; */}
    public VRAvatarHand RightHand { get => rightHand;/* set => rightHand = value;*/ }
    public static UserInputs Instance { get => instance;}
    
    

    //VR controller inputs here

}