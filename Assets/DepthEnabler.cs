using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DepthEnabler : MonoBehaviour
{
    public Camera[] cameras;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < cameras.Length; i++)
        {
            cameras[i].depthTextureMode = DepthTextureMode.Depth;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
