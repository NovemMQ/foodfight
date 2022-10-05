using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandGrips : MonoBehaviour
{
    [SerializeField]
    private Transform foreGrip;
    [SerializeField]
    private Transform backGrip;
    private float initialSize;
    // Start is called before the first frame update
    private void Awake()
    {
        initialSize = Vector3.Magnitude(backGrip.position - foreGrip.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        SetPos(backGrip.position, foreGrip.position);
    }

    private void SetPos(Vector3 start, Vector3 end)
    {
        var dir = end - start;
        var mid = (dir) / 2.0f + start;
        transform.position = mid;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        Vector3 scale = transform.localScale;
        scale.z = dir.magnitude * 0.5f;
        transform.localScale = scale;
    }
}
