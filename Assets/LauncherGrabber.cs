using Liminal.SDK.Input;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherGrabber : MonoBehaviour
{
    private bool isHeld=false;
    public bool isBack = false;
    private void Start()
    {
        if (isBack)
            UserInputs.Instance.LeftHandAvatarHand.Attach(this.gameObject);
        else
            UserInputs.Instance.RightHandAvatarHand.Attach(this.gameObject);
    }
 /*   private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("TriggerActivated");
        VRAvatarHand hand = other.gameObject.GetComponent<VRAvatarHand>();
        if (!isHeld)
        {
            if (hand != null && (UserInputs.Instance.LeftHand.GetButtonDown(VRButton.One)|| UserInputs.Instance.RightHand.GetButtonDown(VRButton.One)))
            {
                this.transform.SetParent(hand.transform);
                isHeld = true;
                Debug.Log("button works");
            }
        }
    }*/
}
