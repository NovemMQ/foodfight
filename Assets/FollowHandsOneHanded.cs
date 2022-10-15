using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandsOneHanded : MonoBehaviour
{
    [SerializeField]
    Transform HandHook;
    [SerializeField]
    FollowHandsOneHanded Gun;
    [SerializeField]
    FollowHandsOneHanded Shield;
  
    private void FixedUpdate()
    {
        transform.position = HandHook.position + (Vector3.down*0.05f);
        transform.rotation = HandHook.rotation;
        transform.Rotate(Vector3.up, 180f);

    }
}
