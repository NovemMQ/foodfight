using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandsOneHanded : MonoBehaviour
{
    [SerializeField]
    Transform HandHook;

    private void FixedUpdate()
    {
        SetPos();
    }

    private void SetPos()
    {
        transform.position = HandHook.position;
        transform.SetParent(HandHook);
    }

}
