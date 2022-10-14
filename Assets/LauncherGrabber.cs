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
    VRAvatarHand hand;
    public bool isBack = false;

    public VRAvatarHand Hand { get => hand;}

    private void Start()
    {
        
        hand.Attach(this.gameObject);
    }
    
}
