using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRaycastScript : MonoBehaviour
{
    [SerializeField] float m_GroundCheckDistance = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    RaycastHit hitInfomation;
  
    public RaycastHit HitInfomation { get => hitInfomation; set => hitInfomation = value; }

    public bool CheckGroundRaycast()
    {
        RaycastHit hitInfo;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + (Vector3.down * m_GroundCheckDistance);
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(startPos, endPos, Color.red);
#endif
        
        if(Physics.Raycast(startPos, Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            HitInfomation = hitInfo;
            return true;
        } else
        {
            return false;
        }
       
    }
}
