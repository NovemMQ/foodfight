﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandsOneHanded : MonoBehaviour
{
    [SerializeField]
    Transform HandHook;
    private void Start()
    {
    }
    private void FixedUpdate()
    {
        transform.position = HandHook.position;
        transform.rotation = HandHook.rotation;
        transform.Rotate(Vector3.up, 180f);
        
    }

    

}
