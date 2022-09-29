using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectooryTrace : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    [Range(3, 30)]
    private float lineSegmentCount = 20;

    private List<Vector3> linePoints = new List<Vector3>();
    private bool allowTrajectory=false;

    public bool AllowTrajectory { get => allowTrajectory; set => allowTrajectory = value; }

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        Vector3 velocity =forceVector;
        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = FlightDuration / lineSegmentCount;
        linePoints.Clear();
        for(int i=0; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;
            Vector3 MovementVector = new Vector3(velocity.x * stepTimePassed, velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed, velocity.z * stepTimePassed);
            linePoints.Add(-MovementVector + startingPoint);
        }
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
}
