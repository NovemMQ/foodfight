﻿using Liminal.SDK.Input;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherGrabber : MonoBehaviour
{
    private bool isHeld=false;
    private void OnTriggerEnter(Collider other)
    {
        
        VRAvatarHand hand = other.gameObject.GetComponent<VRAvatarHand>();
        if (!isHeld)
        {
            if (hand != null && UserInputs.Instance.LeftHand.GetButtonDown(VRButton.One))
            {
                this.transform.SetParent(hand.transform);
                isHeld = true;
            }
        }
    }
}
