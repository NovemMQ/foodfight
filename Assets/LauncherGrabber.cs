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
    [SerializeField]
    VRAvatarHand Hand;
    public bool isBack = false;
    private void Start()
    {
        
        Hand.Attach(this.gameObject);
    }
    
}
