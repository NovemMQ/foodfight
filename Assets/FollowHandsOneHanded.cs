using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandsOneHanded : MonoBehaviour
{
    [SerializeField]
    Transform HandHook;
    private void Awake()
    {
        transform.position = HandHook.position;
    }
    private void FixedUpdate()
    {
        SetPos();
    }

    private void SetPos()
    {
        transform.SetParent(HandHook);
    }

}
