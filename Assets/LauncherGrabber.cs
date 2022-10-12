using Liminal.SDK.Input;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherGrabber : MonoBehaviour
{
    [SerializeField]
    VRAvatarController controller;
    public bool isBack = false;
    private void Start()
    {
        
        //UserInputs.Instance.LeftHandAvatarHand.Attach(this.gameObject);
    }
    private void FixedUpdate()
    {
        transform.position = controller.transform.position;
        transform.rotation = controller.transform.rotation;
    }
}
